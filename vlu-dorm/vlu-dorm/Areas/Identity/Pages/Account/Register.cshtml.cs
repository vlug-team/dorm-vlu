// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using vlu_dorm.Data;
using vlu_dorm.Models;
using vlu_dorm.Services;

namespace vlu_dorm.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext context;
        private readonly StudentServices student;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext _context,
            StudentServices services,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            context = _context;
            student = services;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<Students> Emails { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Roles = _roleManager.Roles.ToList();
            Emails = context.Students.Where(c => c.IsConfirm == true && c.IsActive == false).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/quan-ly");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var userInput = student.GetStudentsByEmail(Input.Email);
                var Password = $"Vlu@{userInput.StudentCode}";
                var user = new ApplicationUser { UserName = Input.Email, FullName = userInput.FullName, Email = Input.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    Success(Input.Email, user);
                    _logger.LogInformation("User created a new account with password.");
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                _logger.LogInformation("Đăng kí không thành công");
                return Page();
            }
            return Page();

        }
        private void Success(string email, ApplicationUser user)
        {
            var student = context.Students.Where(c => c.Email == email).FirstOrDefault();
            student.IsActive = true;
            student.UserNavgation = user;
            context.Update(student);
            context.SaveChanges();

        }

    }

}

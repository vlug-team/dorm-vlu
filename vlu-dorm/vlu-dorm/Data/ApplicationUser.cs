using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityUser = Microsoft.AspNet.Identity.EntityFramework.IdentityUser;

namespace vlu_dorm.Data
{
    public class ApplicationUser : IdentityUser
    {
    }
    public class ApplicationUserRole : IdentityUserRole
    {
        public override string? UserId { get; set; }
        public override string? RoleId { get; set; }
    }
}

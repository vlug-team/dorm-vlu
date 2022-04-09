using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using vlu_dorm.Models;

namespace vlu_dorm.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public virtual Students StudentsNavigation { get; set; }
        public virtual ICollection<Students> Students { get; set; }
    }

}

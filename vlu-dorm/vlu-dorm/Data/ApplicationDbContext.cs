using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace vlu_dorm.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }
     

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Create roles Admin && User
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = "4aac28af-6ded-4720-94cf-bab3cb4072e9", ConcurrencyStamp = Guid.NewGuid().ToString() });
            //Create admin account Email: admin@gmail.com password: Admin@123
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser { Id = "33e90769-dc14-43cd-a62d-baac45c91ae7", UserName = "Admin", Email = "admin@gmail.com", NormalizedEmail = "ADMIN@GMAIL.COM", NormalizedUserName = "ADMIN", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEEY/DHwYSPjx4vi8Kb2B2+anD5UAzEbSFpV1VRGMwd2Ektf2T1zpluc0yPVYO4f/hg==", SecurityStamp = "L72NQFAWWWW7OX2PMA7MHUQ3KMCEBWUM", ConcurrencyStamp = "46cf4368-64a0-45ba-a804-61358816c6b3" });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = "4aac28af-6ded-4720-94cf-bab3cb4072e9", UserId = "33e90769-dc14-43cd-a62d-baac45c91ae7" });

        }
    }
}
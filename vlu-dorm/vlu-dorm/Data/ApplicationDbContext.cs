using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vlu_dorm.Models;

namespace vlu_dorm.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }

        public DbSet<Students> Students { set; get; }
        public DbSet<Room> Rooms { set; get; }
        public DbSet<BillMonthly> BillMonthlies { set; get; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Add the shadow property to the Student class
            builder.Entity<Room>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasMany(c => c.StudentsNavgation).WithOne(c => c.RoomNavgation).HasForeignKey(c => c.RoomId).HasConstraintName("fk_Student_Room");
            });
            builder.Entity<BillMonthly>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasMany(c => c.StudentsNavgation).WithOne(c => c.BillMonthlyNavgation).HasForeignKey(c => c.BillId).HasConstraintName("fk_Student_Bill");
            });


            //Create roles Admin && User
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = "4aac28af-6ded-4720-94cf-bab3cb4072e9", ConcurrencyStamp = Guid.NewGuid().ToString() });
            //Create admin account Email: admin@gmail.com password: Admin@123
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser { Id = "33e90769-dc14-43cd-a62d-baac45c91ae7", UserName = "Admin", Email = "admin@gmail.com", NormalizedEmail = "ADMIN@GMAIL.COM", NormalizedUserName = "ADMIN", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEEY/DHwYSPjx4vi8Kb2B2+anD5UAzEbSFpV1VRGMwd2Ektf2T1zpluc0yPVYO4f/hg==", SecurityStamp = "L72NQFAWWWW7OX2PMA7MHUQ3KMCEBWUM", ConcurrencyStamp = "46cf4368-64a0-45ba-a804-61358816c6b3" });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = "4aac28af-6ded-4720-94cf-bab3cb4072e9", UserId = "33e90769-dc14-43cd-a62d-baac45c91ae7" });
            base.OnModelCreating(builder);
        }
    }
}
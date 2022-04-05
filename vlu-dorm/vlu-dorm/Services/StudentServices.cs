using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using vlu_dorm.Data;
using vlu_dorm.Models;

namespace vlu_dorm.Services
{
    public class StudentServices : PageModel
    {
        readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public StudentServices(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }
        public List<Students> GetAll()
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Students.Include(c => c.RoomNavgation).Include(c=>c.RoomNavgation.BillNavgation).ToList();
        }
        public List<Students> GetAllById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Students.Where(c => c.Id == id).Include(c => c.RoomNavgation).ToList();
        }
        public Students GetStudentsByEmail(string email)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Students.FirstOrDefault(c => c.Email == email);
        }
        public Students GetStudentById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Students.FirstOrDefault(c => c.Id == id);
        }
        public async Task AddAsync(Students students)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Students.Add(students);
            await context.SaveChangesAsync();
        }
        public void Confirm(Students student)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Students.Update(student);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var student = context.Students.Where(c => c.Id == id).First();
            var account = context.Users.Where(c => c.Email == student.Email).First();
            context.Users.Remove(account);
            context.Students.Remove(student);
            context.SaveChanges();
        }
        public async Task UpdateAsync(Students students)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Students.Update(students);
            await context.SaveChangesAsync();
        }
        public void Update(Students students)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Students.Update(students);
            context.SaveChanges();
        }
    }

}

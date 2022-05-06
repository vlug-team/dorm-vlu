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
            return context.Students
                .Include(c => c.RoomNavgation)
                .Include(c => c.RoomNavgation.BillNavgation)
                .ToList();
        }

        public List<Students> GetAllById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Students
                .Where(c => c.Id == id)
                .Include(c => c.RoomNavgation)
                .Include(c => c.RoomNavgation.BillNavgation)
                .ToList();
        }

        public List<Students> GetAllByEmail(string email)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Students
                .Where(c => c.Email == email)
                .Include(c => c.RoomNavgation)
                .Include(c => c.RoomNavgation.BillNavgation)
                .ToList();
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
            context.Students.Remove(student);
            context.SaveChanges();
        }

        public void DeleteAccount(string email)
        {
            using var context = _contextFactory.CreateDbContext();
            var account = context.Users.Where(c => c.Email == email).FirstOrDefault();
            if (account != null)
            {
                var student = context.Students.Where(c => c.Email == email).FirstOrDefault();
                context.Students.Remove(student);
                context.Users.Remove(account);
                context.SaveChanges();
            }
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

        //get money in mont of student same room

        public double GetMoney(int id)
        {
            double sum = 0;
            using var context = _contextFactory.CreateDbContext();
            var student = context.Students.Where(c => c.Id == id).FirstOrDefault();
            int bills = context.BillMonthlies.Where(c => c.Id == student.RoomId).Count();
            sum =
                (student.BikeNumber * student.RoomNavgation.BikePrice)
                + student.RoomNavgation.RoomPrice
                + (
                    (
                        (
                            student.RoomNavgation.ElectricPrice
                            * student.RoomNavgation.BillNavgation.ElectricNumber
                        )
                        + (
                            student.RoomNavgation.BillNavgation.WaterNumber
                            * student.RoomNavgation.WaterPrice
                        )
                    ) / bills
                );
            return sum;
        }
    }
}

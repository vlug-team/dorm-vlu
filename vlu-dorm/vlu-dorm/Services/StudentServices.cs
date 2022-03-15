using Microsoft.EntityFrameworkCore;
using vlu_dorm.Data;
using vlu_dorm.Models;

namespace vlu_dorm.Services
{
    public class StudentServices
    {
        readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        public StudentServices(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }
        public async Task<List<Students>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Students.ToListAsync();
        }
        public void Add(Students students)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Students.Add(students);
            context.SaveChanges();
        }
    }

}

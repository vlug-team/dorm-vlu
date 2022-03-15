using Microsoft.EntityFrameworkCore;
using vlu_dorm.Data;
using vlu_dorm.Models;

namespace vlu_dorm.Services
{
    public class StudentServices
    {
        private readonly DormDbContext _context;

        public StudentServices(DormDbContext context)
        {
            _context = context;
        }

        public void Add(Students students)
        {
            _context.Students.Add(students);
            _context.SaveChanges();
        }
        public void SaveChanges()
        {
            _context.SaveChangesAsync();
        }
        public List<Students> GetAll()
        {
            return _context.Students.Include(c => c.Id).ToList();
        }

        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using vlu_dorm.Data;
using vlu_dorm.Models;

namespace vlu_dorm.Services
{
    public class RoomServices
    {
        readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        public RoomServices(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }
        public List<Room> GetAll()
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Rooms.ToList();
        }
        public List<Room> GetAllAndBill()
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Rooms.Include(c=>c.StudentsNavgation).ToList();
        }
        public Room GetRoomByID(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Rooms.Include(c=>c.BillNavgation).FirstOrDefault(c => c.Id == id);
        }
        public void UpdateRoom(Room room)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Rooms.Update(room);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var room = context.Rooms.Find(id);
            context.Rooms.Remove(room);
            context.SaveChanges();
        }
        public async Task AddRoomAsync(Room room)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Rooms.Add(room);
            await context.SaveChangesAsync();
        }
    }
}

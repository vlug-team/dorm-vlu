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
    
        public Room GetRoomByID(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Rooms.Include(c=>c.BillNavgation).FirstOrDefault(c => c.Id == id);
        }
        public List<Room> GetBillMonthlies()
        {
            using var context = _contextFactory.CreateDbContext();
            var room = context.Rooms.Include(c => c.BillNavgation).ThenInclude(c => c.RoomsNavgation).Where(c => c.BillNavgation.BillMonth.Month == DateTime.Today.Month).ToList();
            if (room == null)
            {
                return null;
            }
            return room;
        }
        public void UpdateRoom(Room room)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Rooms.Update(room);
            context.SaveChanges();
        }
        public async Task UpdateRoomAsync(Room room)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Rooms.Update(room);
            await context.SaveChangesAsync();
        }
        public int GetCountStudentSameRoom(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            if (context.Rooms.FirstOrDefault(c => c.Id == id) == null)
            {
                return 0;
            }
            return context.Rooms.Where(c=>c.Id == id).ToList().Count;
        }
        public void Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var room = context.Rooms.Where(c=>c.Id == id).FirstOrDefault();
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

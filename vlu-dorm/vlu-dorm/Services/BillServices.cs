
using Microsoft.EntityFrameworkCore;
using vlu_dorm.Data;
using vlu_dorm.Models;

namespace vlu_dorm.Services
{
    public class BillServices
    {
        readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public BillServices(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddBill(BillMonthly bill)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.BillMonthlies.Add(bill);
                context.SaveChanges();
            }
        }
        public async Task UpdateBill(BillMonthly bill)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.BillMonthlies.Update(bill);
                await context.SaveChangesAsync();
            }

        }

    }
}

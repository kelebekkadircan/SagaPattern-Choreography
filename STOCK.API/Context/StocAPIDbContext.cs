using Microsoft.EntityFrameworkCore;
using STOCK.API.Entities;

namespace STOCK.API.Context
{
    public class StocAPIDbContext : DbContext
    {
        public StocAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }

        
    }
}

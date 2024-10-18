using Microsoft.EntityFrameworkCore;
using VerticalSlicingApi.Domain;

namespace VerticalSlicingApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> option):DbContext(option)
    {
        public DbSet<Product> Products => Set<Product>();
    }
}

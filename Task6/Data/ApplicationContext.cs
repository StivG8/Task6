using Microsoft.EntityFrameworkCore;
using Task6.Models;

namespace Task6.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Element> Elements { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

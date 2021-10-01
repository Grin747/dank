using DankContainers.Models;
using Microsoft.EntityFrameworkCore;

namespace DankContainers.Data
{
    public class HelloContext : DbContext
    {
        public HelloContext(DbContextOptions<HelloContext> options)
            : base(options)
        {
        }

        public DbSet<Model> Models { get; set; }
    }
}
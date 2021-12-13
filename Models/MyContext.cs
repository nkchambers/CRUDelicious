using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) {}

        
        // MAKE SURE you are adding a DbSet for every single model
        // you wish to translate to the database
        public DbSet<Dish> Dishes { get; set; }
    }
}
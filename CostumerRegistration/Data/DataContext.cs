using Microsoft.EntityFrameworkCore;

namespace CostumerRegistration.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        
        }

        public DbSet<Costumer> Costumers { get; set; }
    }
}

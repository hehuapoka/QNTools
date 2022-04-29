using Microsoft.EntityFrameworkCore;
namespace QNWebServer
{
    public class PizzaStoreContext:DbContext
    {
        public PizzaStoreContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<PizzaSpecial> Specials {get;set;}
    }
}
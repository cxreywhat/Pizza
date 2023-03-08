namespace Pizza.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<PizzaModel> Pizzas => Set<PizzaModel>();
        public DbSet<UserModel> Users => Set<UserModel>();
    }
}
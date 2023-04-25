namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) // 1. The DataContext constructor accepts a DbContextOptions parameter. The DbContextOptions parameter is used to configure the context. The base keyword is used to call the constructor of the base class (DbContext).
        {

        }

        public DbSet<Character> Characters => Set<Character>(); // 2. The DataContext class contains a DbSet property for each entity set. In this example, there is a DbSet property for the Character entity set. Entity sets typically correspond to database tables, and an entity corresponds to a row in the table.
    }
}
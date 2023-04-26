# Table of Contents
- Save data in database with Entity Framework Core
- All CRUD operations with EF Core
- Persistence
- Postgres
- Code-First Migration
- Asynchronous operations

# Object Relational Mapping (ORM)
Object Relational Mapping (ORM) is a technique that helps you interact with a database using an object-oriented approach rather than writing SQL queries directly.

In the context of a .NET Web API, an ORM like Entity Framework Core allows you to perform database operations using C# classes and objects, which are then automatically converted to SQL queries to interact with the database.

Adding EF Core to your project:

```bash
    dotnet add package Microsoft.EntityFrameworkCore
```

Adding PostgreSQL support to your project:

```bash
    dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

Adding EF Core design support to your project:

```bash
    dotnet add package Microsoft.EntityFrameworkCore.Design
```

Install the dotnet-ef tool:

```bash
    dotnet tool install --global dotnet-ef
```

(Optional) Add DotNetEnv to your project, this allows you to use environment variables from `.env` file in your project:

```bash
    dotnet add package DotNetEnv
```

# Code First Migration
Code First Migration is a feature of Entity Framework Core that allows you to create and update the database schema based on the changes in your model classes, instead of manually altering the database tables. When you create or modify your models, Code First Migration generates the necessary SQL scripts to apply those changes to the database schema.

## Getting started

1. Add DataContext class:

    ```csharp
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products => Set<Product>();
    }
    ```
2. Add connection string to appsettings.json:

    ```json
    {
        "ConnectionStrings": {
            "DefaultConnection": "Host=localhost;Port=5432;Database=${POSTGRES_DB};Username=${POSTGRES_USER};Password=${POSTGRES_PASSWORD}"
        },
        // Rest of the configuration
    }
    ```
3. Add service to `Program.cs`:

    ```csharp
    builder.Services.AddDbContext<DataContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    ```

4. Add migration:

    ```bash
    dotnet ef migrations add InitialCreate
    ```

5. Update database:

    ```bash
    dotnet ef database update
    ```

# Update CRUD operations to update DB with EF Core

1. Update service class to use `DataContext` instead of in-mermory list to CRUD

   ```csharp
    public class ProductService : IProductService
    {
         private readonly DataContext _context;
    
         public ProductService(DataContext context)
         {
              _context = context;
         }
    
         public async Task<Product> CreateProductAsync(Product product)
         {
              _context.Products.Add(product);
              await _context.SaveChangesAsync();
              return product;
         }
    
         public async Task DeleteProductAsync(int id)
         {
              var product = await _context.Products.FindAsync(id);
              _context.Products.Remove(product);
              await _context.SaveChangesAsync();
         }
    
         public async Task<IEnumerable<Product>> GetProductsAsync()
         {
              return await _context.Products.ToListAsync();
         }
    
         public async Task<Product> GetProductByIdAsync(int id)
         {
              return await _context.Products.FindAsync(id);
         }
    
         public async Task UpdateProductAsync(Product product)
         {
              _context.Entry(product).State = EntityState.Modified;
              await _context.SaveChangesAsync();
         }
    }
    ```

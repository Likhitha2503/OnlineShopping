using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;

namespace OnlineShopping_API.DataStore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Clothing",
                    
                    
       
                },
              new Category
              {
                  Id = 2,
                  Name = "Mobiles",
                 
                 
              },
              new Category
              {
                  Id = 3,
                  Name = "Groceries",
                  
                 
              },
              new Category
              {
                  Id = 4,
                  Name = "Electronics",
                  
                  
              },
              new Category
              {
                  Id = 5,
                  Name = "Stationery",
                
                  
              });
        }
    }
}

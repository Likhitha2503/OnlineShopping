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
                    ImageUrl = ""
                    
       
                },
              new Category
              {
                  Id = 2,
                  Name = "Mobiles",
                  ImageUrl = ""
                 
              },
              new Category
              {
                  Id = 3,
                  Name = "Groceries",
                  ImageUrl = ""
                 
              },
              new Category
              {
                  Id = 4,
                  Name = "Electronics",
                  ImageUrl = ""
                  
              },
              new Category
              {
                  Id = 5,
                  Name = "Stationery",
                  ImageUrl = "",
                  
              });
        }
    }
}

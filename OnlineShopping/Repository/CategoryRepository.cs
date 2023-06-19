using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;
using OnlineShopping_API.DataStore;
using OnlineShopping_API.Repository.IRepository;
using System.Linq.Expressions;

namespace OnlineShopping_API.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        


       

        public async  Task<Category> UpdateAsync(Category entity)
        {
            _db.Categories.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
    


using OnlineShopping.Models;
using System.Linq.Expressions;

namespace OnlineShopping_API.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
    
        Task<Category> UpdateAsync(Category entity);
    }
}

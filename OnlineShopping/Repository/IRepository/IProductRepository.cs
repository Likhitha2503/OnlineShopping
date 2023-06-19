using OnlineShopping.Models;
using System.Linq.Expressions;

namespace OnlineShopping_API.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
    
        Task<Product> UpdateAsync(Product entity);
    }
}

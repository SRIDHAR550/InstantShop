using InstantShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantShop.Domain.Contracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task UpdateAsync(Product product);
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetDetailsAsync(int id);
    }
}

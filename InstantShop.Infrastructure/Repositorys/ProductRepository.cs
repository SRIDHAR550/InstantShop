using InstantShop.Domain.Contracts;
using InstantShop.Domain.Model;
using InstantShop.Infrastructure.DbConnections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantShop.Infrastructure.Repositorys
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _dbContext.Product.Include(x => x.Category).Include(x => x.Brand).AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetDetailsAsync(int id)
        {
            return await _dbContext.Product.Include(x => x.Category).Include(x => x.Brand).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}

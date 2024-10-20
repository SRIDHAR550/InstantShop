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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task UpdateAsync(Category category)
        {
            _dbContext.Category.Update(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}

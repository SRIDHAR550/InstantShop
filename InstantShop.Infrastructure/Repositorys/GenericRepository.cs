using InstantShop.Domain.Common;
using InstantShop.Domain.Contracts;
using InstantShop.Infrastructure.DbConnections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InstantShop.Infrastructure.Repositorys
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;  
        }

        public async Task<T> CreateAsync(T entity)
        {
           var CrateBrand=  await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return CrateBrand.Entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> Condition)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(Condition);
        }
      
    }
}

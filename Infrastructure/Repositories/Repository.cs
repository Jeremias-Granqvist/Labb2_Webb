using Labb2_Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Repositories
{
    public class Repository<Tentity> : IRepository<Tentity>
        where Tentity : class
    {
        protected readonly StoreContext dbContext;
        protected readonly DbSet<Tentity> dbSet;

        public Repository(StoreContext context)
        {
            dbContext = context;
            dbSet = context.Set<Tentity>();
        }

        public async Task<Tentity> AddAsync(Tentity entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete == null)
            {
                return false;
            }

            dbSet.Remove(entityToDelete);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Tentity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<Tentity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Tentity entity)
        {
            var trackedEntity = dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}

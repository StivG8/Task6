using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task6.Data;

namespace Task6.Repositories
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        readonly ApplicationContext context;
        readonly DbSet<TEntity> dbSet;

        public EFGenericRepository(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> Create(TEntity item)
        {
            await dbSet.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task<TEntity> FindById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> Remove(TEntity item)
        {
            dbSet.Remove(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task<TEntity> Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return item;
        }
    }
}

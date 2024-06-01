using DmStore.Data;
using DmStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DmStore.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetListAllItensAsync();
        Task<T> GetItemByIdAsync(string id);
        Task<IEnumerable<T>> GetItemByDescriptionAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ItemExistsAsync(string id);
        Task<T> AddItemAsync(T entity);
        Task UpdateItem(T entity);
        void RemoveItem(T entity);
        Task<int> CommitAsync();
        void Dispose();
    }

    public class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly DmStoreDbContext _context;
        protected readonly DbSet<T> _dbSetContext;

        public Repository(DmStoreDbContext context)
        {
            _context = context;
            _dbSetContext = context.Set<T>();
        }

        public virtual async Task<List<T>> GetListAllItensAsync()
        {
            return await _dbSetContext.ToListAsync();
        }

        public virtual async Task<T> GetItemByIdAsync(string id)
        {
            return await _dbSetContext.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetItemByDescriptionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSetContext.Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual async Task<bool> ItemExistsAsync(string id)
        {
            return await _dbSetContext.AnyAsync(x => x.ID == id);
        }

        public virtual async Task<T> AddItemAsync(T entity)
        {
            await _dbSetContext.AddAsync(entity);
            await CommitAsync();
            return entity;
        }

        public virtual async Task UpdateItem(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await CommitAsync();
        }

        public virtual async void RemoveItem(T entity)
        {
            _dbSetContext.Remove(entity);
            await CommitAsync();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

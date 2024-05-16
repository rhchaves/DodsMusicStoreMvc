using DmStore.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DmStore.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetListItemAsync();
        Task<T> GetItemByIdAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddItemAsync(T entity);
        void UpdateItem(T entity);
        void DeleteItem(T entity);
        Task<bool> ItemExistsAsync(Expression<Func<T, bool>> predicate);
        void CommitAsync();
        void Dispose();
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        protected DmStoreDbContext _context;

        public Repository(DmStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetListItemAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetItemByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public async Task<T> AddItemAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            CommitAsync();
            return entity;
        }

        public void DeleteItem(T entity)
        {
            _context.Set<T>().Remove(entity);
            CommitAsync();
        }

        public void UpdateItem(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            CommitAsync();
        }

        public async Task<bool> ItemExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async void CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

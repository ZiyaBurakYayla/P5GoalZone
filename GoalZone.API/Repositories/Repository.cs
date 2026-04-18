using GoalZone.API.Data;
using GoalZone.API.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GoalZone.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly GoalZoneDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(GoalZoneDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
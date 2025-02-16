using BuildMasterPro.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BuildMasterPro.Repositories
{
    public class RepositoryBased<T> : IRepositoryBased<T> where T : class
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;
        internal DbSet<T> _dbSet;
        internal string keyName;
        internal string tableName;
        
        public RepositoryBased(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
            ApplicationDbContext _db = _context.CreateDbContext();
            var entityType = _db.Model.FindEntityType(typeof(T));
            this._dbSet = _db.Set<T>();
            this.keyName = entityType.FindPrimaryKey().Properties.Select(o => o.Name).Single();
            var schema = entityType.GetSchema();
            tableName = entityType.GetTableName();
        }
        public async Task<T> AddAsync(T entity)
        {
            using var _db = _context.CreateDbContext();
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> order = null, bool isAsc = true, bool isTracked = true, Expression<Func<T, object>> include = null, Expression<Func<T, object>> include2 = null)
        {
            using var _db = _context.CreateDbContext();
            IQueryable<T> query = _dbSet;
            if (!isTracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (isAsc && order != null)
            {
                query = query.OrderBy(order);
            }
            else if (!isAsc && order != null)
            {
                query = query.OrderByDescending(order);
            }

            if (include != null)
            {
                query = query.Include(include);
            }

            if (include2 != null)
            {
                query = query.Include(include2);
            }

            return await query
                .ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> order = null, bool isAsc = true, bool isTracked = true, Expression<Func<T, object>> include = null)
        {
            using var _db = _context.CreateDbContext();
            IQueryable<T> query = _dbSet;
            if (!isTracked)
            {
                query = query.AsNoTracking();
            }

            if (isAsc && order != null && filter != null)
            {
                query = query
                    .OrderBy(order);
            }
            else if (!isAsc && order != null)
            {
                query = query
                    .OrderByDescending(order);
            }

            if (include != null)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task UpdateAsync(T oldEntity, T newEntity)
        {
            using var _db = _context.CreateDbContext();
            _db.Entry(oldEntity).CurrentValues.SetValues(newEntity);
            await _db.SaveChangesAsync();
        }
    }
}

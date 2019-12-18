using Movies.Data.Core.Models;
using Movies.Data.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Movies.Data.Db.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;
        public DbSet<T> _dbSet;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbSet = _dbContext.Set<T>();
        }

        #region Sync Operations
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(long Id)
        {
            return _dbSet.Find(Id);
        }

        public T Create(T entity)
        {
            var e = _dbSet.Add(entity);

            Save();

            return e.Entity;
        }

        public T Update(T entity)
        {
            var e = _dbSet.Update(entity);

            Save();

            return e.Entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);

            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        #endregion

        #region Async Operations
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            var e = _dbSet.Add(entity);

            await SaveAsync();

            return e.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var e = _dbSet.Update(entity);

            await SaveAsync();

            return e.Entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}

using Movies.Data.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(long Id);
        T Create(T entity);
        void Delete(T entity);
        T Update(T entity);
        void Save();

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(long Id);
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task SaveAsync();
    }
}

using Movies.Data.Core.Models;
using Movies.Data.Core.Repositories;
using Movies.Data.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Movies.Data.Db.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext dbContext) : base(dbContext)
        { }

        public Movie GetByTitle(string title)
        {
            return base._dbSet.FirstOrDefault(q => q.Title == title || q.Title.Contains(title));
        }

        public Task<Movie> GetByTitleAsync(string title)
        {
            return base._dbSet.FirstOrDefaultAsync(q => q.Title == title || q.Title.Contains(title));
        }

        public IEnumerable<Movie> GetAllByTitle(string title)
        {
            return _dbSet.Where(q => q.Title == title || q.Title.Contains(title)).ToList();
        }

        public async Task<IEnumerable<Movie>> GetAllByTitleAsync(string title)
        {
            return await _dbSet.Where(q => q.Title == title || q.Title.Contains(title)).ToListAsync();
        }

    }
}

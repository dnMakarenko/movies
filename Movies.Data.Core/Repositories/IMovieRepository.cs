using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Movies.Data.Core.Models;

namespace Movies.Data.Core.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<Movie> GetByTitleAsync(string title);

        Movie GetByTitle(string title);

        Task<IEnumerable<Movie>> GetAllByTitleAsync(string title);

        IEnumerable<Movie> GetAllByTitle(string title);
    }
}

using Movies.Data.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data.Core.Services
{
    public interface IMovieService : IService<Movie>
    {
        Movie GetByTitle(string title);

        Task<Movie> GetByTitleAsync(string title);

        IEnumerable<Movie> GetAllByTitle(string title);

        Task<IEnumerable<Movie>> GetAllByTitleAsync(string title);
    }
}

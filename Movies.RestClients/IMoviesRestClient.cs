using Movies.Dtos.Rest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.RestClients
{
    public interface IMoviesRestClient
    {
        Task<MovieDto> GetMovieAsync(string title);
    }
}

using Movies.Data.Core.Models;
using Movies.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository MovieRepository { get; }
        Task<int> CommitAsync();
    }
}

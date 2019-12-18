using Movies.Data.Core;
using Movies.Data.Core.Repositories;
using Movies.Data.Db.Repositories;
using System.Threading.Tasks;

namespace Movies.Data.Db
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _Dbcontext;

        private MovieRepository _contactRepository;

        public UnitOfWork(AppDbContext Dbcontext)
        {
            this._Dbcontext = Dbcontext;
        }
        public IMovieRepository MovieRepository => _contactRepository = _contactRepository ?? new MovieRepository(_Dbcontext);

        public async Task<int> CommitAsync()
        {
            return await _Dbcontext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _Dbcontext.Dispose();
        }
    }
}

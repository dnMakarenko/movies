using Movies.Data.Core;
using Movies.Data.Core.Models;
using Movies.Data.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Movie Create(Movie entity)
        {
            return _unitOfWork.MovieRepository.Create(entity);
        }

        public async Task<Movie> CreateAsync(Movie entity)
        {
            return await _unitOfWork.MovieRepository.CreateAsync(entity);
        }

        public void Delete(Movie entity)
        {
            _unitOfWork.MovieRepository.Delete(entity);
        }

        public async Task DeleteAsync(Movie entity)
        {
            await _unitOfWork.MovieRepository.DeleteAsync(entity);
        }

        public IEnumerable<Movie> GetAll()
        {
            return _unitOfWork.MovieRepository.GetAll();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _unitOfWork.MovieRepository.GetAllAsync();
        }

        public IEnumerable<Movie> GetAllByTitle(string title)
        {
            return _unitOfWork.MovieRepository.GetAllByTitle(title);
        }

        public async Task<IEnumerable<Movie>> GetAllByTitleAsync(string title)
        {
            return await _unitOfWork.MovieRepository.GetAllByTitleAsync(title);
        }

        public Movie GetById(long Id)
        {
            return _unitOfWork.MovieRepository.GetById(Id);
        }

        public async Task<Movie> GetByIdAsync(long Id)
        {
            return await _unitOfWork.MovieRepository.GetByIdAsync(Id);
        }

        public Movie GetByTitle(string title)
        {
            return _unitOfWork.MovieRepository.GetByTitle(title);
        }

        public Task<Movie> GetByTitleAsync(string title)
        {
            return _unitOfWork.MovieRepository.GetByTitleAsync(title);
        }

        public void Save()
        {
            _unitOfWork.MovieRepository.Save();
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.MovieRepository.SaveAsync();
        }

        public Movie Update(Movie entity)
        {
            return _unitOfWork.MovieRepository.Update(entity);
        }

        public async Task<Movie> UpdateAsync(Movie entity)
        {
            return await _unitOfWork.MovieRepository.UpdateAsync(entity);
        }
    }
}

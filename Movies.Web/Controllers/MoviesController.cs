using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data.Core.Models;
using Movies.Data.Core.Services;
using Movies.RestClients;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMoviesRestClient _moviesRestClient;

        public MoviesController(IMovieService movieService, IMoviesRestClient moviesRestClient)
        {
            _movieService = movieService;
            _moviesRestClient = moviesRestClient;
        }

        // GET: Movies1
        public async Task<IActionResult> Index(string title)
        {
            if (!string.IsNullOrEmpty(title) || !string.IsNullOrWhiteSpace(title))
            {
                var movies = await _movieService.GetAllByTitleAsync(title);
                if (movies == null || movies.Count() == 0)
                {
                    await TryGetMovieFromRemoteAndSave(title);
                }

                return View(await _movieService.GetAllByTitleAsync(title));
            }
            else
            {
                return View(await _movieService.GetAllAsync());
            }
        }

        // GET: Movies1/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Year,Released,Director,Writer,Actors,Language,Country,Poster,ImdbRating,ImgVotes,ImgId,Type,Production,Id")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateAsync(movie);

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies1/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Title,Year,Released,Director,Writer,Actors,Language,Country,Poster,ImdbRating,ImgVotes,ImgId,Type,Production,Id")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _movieService.UpdateAsync(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies1/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            await _movieService.DeleteAsync(movie);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> List()
        {
            return View();
        }

        private bool MovieExists(long id)
        {
            return _movieService.GetById(id) == null ? false: true;
        }

        private async Task TryGetMovieFromRemoteAndSave(string title)
        {
            var remoteMovie = await _moviesRestClient.GetMovieAsync(title);
            
            if (remoteMovie != null)
            {
                var movieEntity = new Data.Core.Models.Movie
                {
                    Actors = remoteMovie.Actors,
                    Country = remoteMovie.Country,
                    Director = remoteMovie.Director,
                    ImdbRating = remoteMovie.ImdbRating,
                    ImgId = remoteMovie.ImgId,
                    ImgVotes = remoteMovie.ImgVotes,
                    Language = remoteMovie.Language,
                    Poster = remoteMovie.Poster,
                    Production = remoteMovie.Production,
                    Released = remoteMovie.Released,
                    Title = remoteMovie.Title,
                    Type = remoteMovie.Type,
                    Writer = remoteMovie.Writer,
                    Year = remoteMovie.Year
                };

                await _movieService.CreateAsync(movieEntity);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Data.Core.Models;
using Movies.Data.Core.Services;
using Movies.RestClients;

namespace Movies.Web.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesApiController : ControllerBase
    {
        private readonly IMoviesRestClient _moviesRestClient;
        private readonly IMovieService _movieService;

        public MoviesApiController(IMoviesRestClient moviesRestClient, IMovieService movieService)
        {
            _moviesRestClient = moviesRestClient;
            _movieService = movieService;
        }

        /// <summary>
        /// Returns movie by title. Example usage: GET: api/Movies/GetByTitle/{title}
        /// </summary>
        /// <param name="title">Represents movie name</param>
        /// <returns>Object typeof(Movie).</returns>
        [HttpGet("GetByTitle/{title}", Name = "GetByTitle")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            try
            {
                var movie = await _movieService.GetByTitleAsync(title);
                if (movie != null)
                {
                    return Ok(movie);
                }

                await TryGetMovieFromRemoteAndSave(title);

                movie = await _movieService.GetByTitleAsync(title);

                return Ok(movie);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Couldn't get movie by title. ErrorMessage:'{e.Message}'");
            }
        }

        /// <summary>
        /// Returns all movies. Example usage: GET: api/Movies
        /// </summary>
        /// <returns>List of movies.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var movies = await _movieService.GetAllAsync();

                return Ok(movies);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Couldn't get movies. ErrorMessage:'{e.Message}'");
            }
        }

        /// <summary>
        /// Returns movie by Id. Exaple usage: GET: api/Movies/5
        /// </summary>
        /// <param name="id">Represents Id of the movie.</param>
        /// <returns>Object typeof(Movie).</returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var movie = await _movieService.GetByIdAsync(id);

                if (movie == null)
                {
                    return NotFound();
                }

                return Ok(movie);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Couldn't get movie by Id. ErrorMessage:'{e.Message}'");
            }
        }

        /// <summary>
        /// Create new movie. Example usage: POST: api/Movies
        /// </summary>
        /// <param name="model">Represents Movie model.</param>
        /// <returns>Created Object typeof(Movie).</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                model.Id = 0;
                var createdMovie = await _movieService.CreateAsync(model);

                if (createdMovie.Id > 0)
                {
                    return CreatedAtAction(nameof(Get), new { id = createdMovie.Id }, createdMovie);
                }

                throw new Exception("Something went wrong while creating movie.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Couldn't create movie. ErrorMessage:'{e.Message}'");
            }
        }

        /// <summary>
        /// Update exiting movie by Id. Example usage: PUT: api/Movies/{id}
        /// </summary>
        /// <param name="id">Represents Id of the movie.</param>
        /// <param name="model">Represents Movie model.</param>
        /// <returns>Updated Object typeof(Movie).</returns>
        // 
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Movie model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (model.Id != id)
                {
                    return BadRequest();
                }

                var movie = await _movieService.GetByIdAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }

                movie = model;
                movie.Id = id;
                movie = await _movieService.UpdateAsync(movie);

                return Ok(movie);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Couldn't update movie. ErrorMessage:'{e.Message}'");
            }
        }

        /// <summary>
        /// Delete movie by Id. Example usage: DELETE: api/Movies/{id}
        /// </summary>
        /// <param name="id">Represents Id of the movie.</param>
        /// <returns>204 if successfully deleted.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try {
                var movie = await _movieService.GetByIdAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }

                await _movieService.DeleteAsync(movie);

                movie = await _movieService.GetByIdAsync(id);

                if (movie == null)
                {
                    return StatusCode(204);
                }

                throw new Exception($"Something went wrong while deleting movie with id:'{id}'");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Couldn't delete movie. ErrorMessage:'{e.Message}'");
            }
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

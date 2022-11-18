using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Trending;
using static IMDB.Domain.DTOs.ApiDTO;
using static IMDB.Domain.DTOs.MovieDTO;

namespace Infrastructure
{
    public interface IMovie
    {
        /// <summary>
        /// returns List of popular movies
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public APIListResult<Movie> GetPopularMovies(int page = 1);
        public Movie GetMovieById(int id);
        /// <summary>
        /// returns a list of movie that realted to the searched txt
        /// </summary>
        /// <param name="txt">search text</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public APIListResult<Movie> SearchMovies(string txt, int page = 1);
        /// <summary>
        /// returns a list of Recomendation movies that relates to movie id
        /// </summary>
        /// <param name="movieId">id of current movie</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public APIListResult<Movie> SimilarMovies(int movieId, int page = 1);

        /// <summary>
        /// post an api for rate movie with this movieId
        /// </summary>
        /// <param name="movieId">Id of Movie</param>
        /// <param name="userId">Current Loged in User id</param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public bool RateMovie(int movieId, int userId, double rate,string SessionId);

        /// <summary>
        /// returns All Genres
        /// </summary>
        public TMDbLib.Objects.Genres.GenreContainer GetAllGenre();

        /// <summary>
        /// Create Guest Session Id For Rate movie
        /// </summary>
        /// <returns></returns>
        public TMDbLib.Objects.Authentication.GuestSession CreateSession();

        /// <summary>
        /// Get trailler of movie
        /// </summary>
        /// <param name="id">id of movie</param>
        /// <returns></returns>
        public TrailersResult GetVideoById(int id);

        public Movie GetLatestMovies();

        public APIListResult<Movie> GetTopRatedMovies(int page = 1);

        public APIListResult<Person> GetPopularPeople(int page = 1);

        public APIListResult<Review> GetReviewsOfMovieById(int id, int page = 1);

        public Credits GetMovieCreditsById(int id);

        public APIListResult<Movie> GetTrendingMovies(MediaType mediaType, TimeWindow period, int page = 1);
    }
}

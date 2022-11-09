using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
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
        public MovieListResault GetPopularMovies(int page = 1);
        public Movie GetMovieById(int id);
        /// <summary>
        /// returns a list of movie that realted to the searched txt
        /// </summary>
        /// <param name="txt">search text</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public MovieListResault SearchMovies(string txt, int page = 1);
        /// <summary>
        /// returns a list of Recomendation movies that relates to movie id
        /// </summary>
        /// <param name="movieId">id of current movie</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public MovieListResault SimilarMovies(int movieId, int page = 1);

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
    }
}

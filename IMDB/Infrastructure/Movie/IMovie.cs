using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Countries;
using TMDbLib.Objects.General;
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
        public Task< APIListResult<Movie>> GetPopularMovies(int page = 1);
        public Task< Movie> GetMovieById(int id);
        /// <summary>
        /// returns a list of movie that realted to the searched txt
        /// </summary>
        /// <param name="txt">search text</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public Task< APIListResult<Movie>> SearchMovies(string txt,int releaseDate, int page = 1);
        /// <summary>
        /// returns a list of Recomendation movies that relates to movie id
        /// </summary>
        /// <param name="movieId">id of current movie</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public Task< APIListResult<Movie>> SimilarMovies(int movieId, int page = 1);

        /// <summary>
        /// post an api for rate movie with this movieId
        /// </summary>
        /// <param name="movieId">Id of Movie</param>
        /// <param name="userId">Current Loged in User id</param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public Task< bool> RateMovie(int movieId, double rate,string SessionId);

        /// <summary>
        /// returns All Genres
        /// </summary>
        public Task< TMDbLib.Objects.Genres.GenreContainer> GetAllGenre();

        /// <summary>
        /// Create Guest Session Id For Rate movie
        /// </summary>
        /// <returns></returns>
        public Task< TMDbLib.Objects.Authentication.GuestSession> CreateSession();

        /// <summary>
        /// Get trailler of movie
        /// </summary>
        /// <param name="id">id of movie</param>
        /// <returns></returns>
        public Task<TrailersResult> GetVideoById(int id);

        public Task< Movie> GetLatestMovies();

        public Task< APIListResult<Movie>> GetTopRatedMovies(int page = 1);

        public Task< APIListResult<Person>> GetPopularPeople(int page = 1);

        public Task< APIListResult<Review>> GetReviewsOfMovieById(int id, int page = 1);

        public Task< TMDbLib.Objects.Movies.Credits> GetMovieCreditsById(int id);

        public Task< APIListResult<Movie>> GetTrendingMovies(MediaType mediaType, TimeWindow period, int page = 1);

        public Task< Person> GetPersonDetailes(int person_id);
        public Task<APIListResult<Movie>> RatedMoviesBySession(string session,int page = 1);
        public Task<APIListResult<Movie>> DiscoverMovie(DiscoverFilterMovie filter);

        public Task<List<Country>> Countries();

        public Task<string> TemporaryRequestToken();
        public Task<string> CreateSession(string requestToken);

        public Task<MovieCredits> GetCreditsByPersonId(int person_id);

    }
}

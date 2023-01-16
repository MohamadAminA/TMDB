using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IMDB.DataLayer;
using IMDB.Domain.DTOs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using TMDbLib.Objects;
using TMDbLib.Objects.Authentication;
using TMDbLib.Objects.Countries;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Genres;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Trending;
using static System.Net.Mime.MediaTypeNames;
using static IMDB.Domain.DTOs.ApiDTO;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Services.Api
{
    public class MovieRepo : IMovie
    {
        private const string key = "646f26e9bfd4f042f28a7160726dd239";
        
        private readonly string api_key = $"api_key={key}";
        private readonly IUser _user;
        private ContextDB _context { get; }
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOption;

        public MovieRepo(IUser user, IMemoryCache cache, ContextDB context)
        {
            _user = user;
            _cache = cache;
            _cacheOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1));
            _context = context;
        }
        public MovieRepo()
        {
        }
        public async Task<GenreContainer> GetAllGenre()
        {
            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/genre/movie/list?{api_key}";
                         
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var Respons = JsonConvert.DeserializeObject<TMDbLib.Objects.Genres.GenreContainer>(strContent);
                return Respons;
            }
            return null;
        }

        public async Task<APIListResult<Movie>> RatedMoviesBySession(string session, int page = 1)
        {
                HttpClient httpClient = new HttpClient();

                string path = $"https://api.themoviedb.org/3/account/0/rated/movies?{api_key}&session_id={session}&sort_by=created_at.desc&page={page}";

                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStreamAsync();

                    using Stream receiveStream = res;
                    using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    string strContent = readStream.ReadToEnd();

                    var Respons = JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                    return Respons;
                }
            
            return null;
        }

        public async Task<Movie> GetMovieById(int id)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/{id}?api_key=646f26e9bfd4f042f28a7160726dd239&language=en-US";
                          
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                Movie movie = JsonConvert.DeserializeObject<Movie>(strContent);
                var rate = _context.Rates.Where(n => n.MovieId == id);
                var countRate = await rate.CountAsync();
                movie.VoteAverage = (movie.VoteAverage * movie.VoteCount + await rate.SumAsync(n => n.Rate)) / (countRate + movie.VoteCount);
                movie.VoteAverage = double.Parse(movie.VoteAverage.ToString(".000"));

                movie.VoteCount += countRate;
                return movie;
            }
            return null;
        }

        public async Task<MovieCredits> GetCreditsByPersonId(int person_id)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/person/{person_id}/movie_credits?{api_key}&language=en-US";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var  result= JsonConvert.DeserializeObject<MovieCredits>(strContent);
                return result;
            }
            return null;
        }


        public async Task<TMDbLib.Objects.Movies.Credits> GetMovieCreditsById(int id)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/{id}/credits?{api_key}&language=en-US";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var Credits = JsonConvert.DeserializeObject<TMDbLib.Objects.Movies.Credits>(strContent);
                return Credits;
            }
            
            return null;
        }

        public async Task<APIListResult<Review>> GetReviewsOfMovieById(int id,int page = 1)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/{id}/reviews?{api_key}&language=en-US&page={page}";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                APIListResult<Review> movieReview = JsonConvert.DeserializeObject<APIListResult<Review>>(strContent);
                return movieReview;
            }
            return null;
        }

        public  async Task<TrailersResult> GetVideoById(int id)
        {

            HttpClient httpClient = new HttpClient();
            string path = $"https://api.themoviedb.org/3/movie/{id}/videos?{api_key}&language=en-US";
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await  httpClient.GetAsync(path);
            TrailersResult trailers = new TrailersResult();
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();
                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();
                trailers = JsonConvert.DeserializeObject<TrailersResult>(strContent);
                return trailers;
            }
            return null;
        }

        public async Task<APIListResult<Movie>> GetPopularMovies(int page = 1)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/popular?{api_key}&language=en-US&page={page}";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                APIListResult<Movie> popMovies = JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                foreach (var movie in popMovies.results)
                {
                    var rate = _context.Rates.Where(n => n.MovieId == movie.Id);
                    var countRate = await rate.CountAsync();
                    movie.VoteAverage = (movie.VoteAverage * movie.VoteCount + await rate.SumAsync(n => n.Rate)) / (countRate + movie.VoteCount);
                    movie.VoteAverage = double.Parse(movie.VoteAverage.ToString(".000"));
                    movie.VoteCount += countRate;
                }
                return popMovies;
            }
            return null;
        }
        public async Task<APIListResult<Movie>> GetTrendingMovies(MediaType mediaType , TimeWindow period,int page = 1)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/trending/{mediaType}/{period}?{api_key}&page={page}";
            path = path.ToLower();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                APIListResult<Movie> popMovies = JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                foreach (var movie in popMovies.results)
                {
                    var rate = _context.Rates.Where(n => n.MovieId == movie.Id);
                    var countRate = await rate.CountAsync();
                    movie.VoteAverage = (movie.VoteAverage * movie.VoteCount + await rate.SumAsync(n => n.Rate)) / (countRate + movie.VoteCount);
                    movie.VoteAverage = double.Parse(movie.VoteAverage.ToString(".000"));
                    movie.VoteCount += countRate;
                }
                return popMovies;
            }
            return null;
        }


        public async Task<APIListResult<Person>> GetPopularPeople(int page = 1)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/person/popular?{api_key}&language=en-US&page={page}";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                APIListResult<Person> popPeople = JsonConvert.DeserializeObject<APIListResult<Person>>(strContent);
                
                return popPeople;
            }
            return null;
        }

        public async Task<APIListResult<Person>> SearchPeople(string query ,int page = 1)
        {

            HttpClient httpClient = new HttpClient();

            string path = $"\r\nhttps://api.themoviedb.org/3/search/person?{api_key}&language=en-US&query={query}&page={page}&include_adult=false";


            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                APIListResult<Person> popPeople = JsonConvert.DeserializeObject<APIListResult<Person>>(strContent);

                return popPeople;
            }
            return null;
        }

        public async Task<Movie> GetLatestMovies()
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/latest?{api_key}&language=en-US";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                Movie popMovies = JsonConvert.DeserializeObject<Movie>(strContent);

                return popMovies;
            }
            return null;
        }

        public async Task<string> TemporaryRequestToken()
        {

            string token = await _cache.GetOrCreateAsync("token", async options =>
            {
                HttpClient httpClient = new HttpClient();


                string path = $"https://api.themoviedb.org/3/authentication/token/new?{api_key}";

                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStreamAsync();

                    using Stream receiveStream = res;
                    using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    string strContent = readStream.ReadToEnd();

                    var resualt = JsonConvert.DeserializeObject<TMDbLib.Objects.Authentication.Token>(strContent);

                    if (resualt.Success)
                    {
                        options.SetOptions(new MemoryCacheEntryOptions().SetSlidingExpiration(new TimeSpan((resualt.ExpiresAt - DateTime.UtcNow).Ticks)));
                        options.SetValue(resualt);
                        return resualt.RequestToken;
                    }
                }
                return null;
            });
            return token;
        }


        public async Task<APIListResult<Movie>> GetTopRatedMovies(int page = 1)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/top_rated?{api_key}&language=en-US&page={page}";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                APIListResult<Movie> popMovies = JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                foreach (var movie in popMovies.results)
                {
                    var rate = _context.Rates.Where(n => n.MovieId == movie.Id);
                    var countRate = await rate.CountAsync();
                    movie.VoteAverage = (movie.VoteAverage * movie.VoteCount + await rate.SumAsync(n => n.Rate)) / (countRate + movie.VoteCount);
                    movie.VoteAverage = double.Parse(movie.VoteAverage.ToString(".000"));
                    movie.VoteCount += countRate;
                }
                return popMovies;
            }
            return null;
        }

        public async Task<bool> RateMovie(int movieId, double rate,string SessionId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();


                string path = $"https://api.themoviedb.org/3/movie/{movieId}/rating?{api_key}&guest_session_id={SessionId}";

                #region Request Body
                var requestData = new Dictionary<string, string>
                {
                    {
                  "value", rate.ToString("0.0")
                }
                };

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(path),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(requestData)
                };
                #endregion


                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStreamAsync();

                    using Stream receiveStream = res;
                    using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    string strContent = readStream.ReadToEnd();

                    var respons = JsonConvert.DeserializeObject<ApiResponse>(strContent);
                    throw new Exception(respons.status_message);
                }
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public async Task<string> CreateSession(string requestToken)
        {
                HttpClient httpClient = new HttpClient();


                string path = $"https://api.themoviedb.org/3/authentication/session/new?{api_key}";

                #region Request Body
                var requestData = new Dictionary<string, string>
                {
                    {
                  "request_token", requestToken
                }
                };

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(path),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(requestData)
                };
                #endregion


                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStreamAsync();

                    using Stream receiveStream = res;
                    using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    string strContent = readStream.ReadToEnd();

                    var respons = JsonConvert.DeserializeObject<UserSession>(strContent);
                    if (respons.Success)
                        return respons.SessionId;
                }
                
                return null;

        }


        public async Task<APIListResult<Movie>> SearchMovies(string txt,int releaseDate = 0, int page = 1)
        {
            HttpClient httpClient = new HttpClient();
            string path = $"https://api.themoviedb.org/3/search/movie?{api_key}&query={txt}&language=en-US&page={page}&include_adult=false";

            if (releaseDate != 0) 
            {
                path += $"&year={releaseDate}";
            }

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var popMovies = JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                foreach (var movie in popMovies.results)
                {
                    var rate = _context.Rates.Where(n => n.MovieId == movie.Id);
                    var countRate = await rate.CountAsync();
                    movie.VoteAverage = (movie.VoteAverage * movie.VoteCount + await rate.SumAsync(n => n.Rate)) / (countRate + movie.VoteCount);
                    movie.VoteAverage = double.Parse(movie.VoteAverage.ToString(".000"));
                    movie.VoteCount += countRate;
                }
                return popMovies;
            }
            return null;
        }

        public async Task<Person> GetPersonDetailes(int person_id)
        {
            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/person/{person_id}?{api_key}&language=en-US";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var person = JsonConvert.DeserializeObject<Person>(strContent);
                return person;
            }
            return null;
        }
        public async Task<APIListResult<Person>> SearchPersons(string text,int page = 1)
        {
            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/search/person?{api_key}&query={text}&page={page}&include_adult=true&language=en-US";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var persons = JsonConvert.DeserializeObject<APIListResult<Person>>(strContent);
                return persons;
            }
            return null;
        }

        public async Task<APIListResult<Movie>> SimilarMovies(int movieId, int page = 1)
        {
            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/{movieId}/similar?{api_key}&language=en-US&page={page}";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var movie = JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                foreach (var movies in movie.results)
                {
                    var rate = _context.Rates.Where(n => n.MovieId == movies.Id);
                    var countRate = await rate.CountAsync();
                    movies.VoteAverage = (movies.VoteAverage * movies.VoteCount + await rate.SumAsync(n => n.Rate)) / (countRate + movies.VoteCount);
                    movies.VoteAverage = double.Parse(movies.VoteAverage.ToString(".000"));
                    movies.VoteCount += countRate;
                }
                return movie;
            }
            return null;
        }

        public async Task<APIListResult<Movie>> DiscoverMovie(DiscoverFilterMovie filter)
        {
            HttpClient httpClient = new HttpClient();


            StringBuilder path = new StringBuilder($"https://api.themoviedb.org/3/discover/movie?{api_key}&region={filter.Region}&sort_by={filter.SortBy}&include_adult={filter.IncludeAdult}&page={filter.Page}&primary_release_date.gte={filter.ReleaseDateFrom}&primary_release_date.lte={filter.ReleaseDateTo}&with_release_type={filter.ReleaseType}&vote_count.gte={filter.VoteCountFrom}&vote_count.lte={filter.VoteCountTo}&vote_average.gte={filter.VoteAverageFrom}&vote_average.lte={filter.VoteAverageTo}&with_runtime.gte={filter.TimeFrom}&with_runtime.lte={filter.TimeTo}&with_people={filter.People}&with_genres={filter.Genres}");
            //if(!=null&& filter.People.Count != 0)
            //{
            //    path.Append("");
            //    foreach (var person in filter.People)
            //    {
            //        path.Append(person);
            //        path.Append(",");
            //    }
            //}
            //if ( != null && filter.Genres.Count != 0)
            //{
            //    path.Append("");
            //    foreach (var genre in filter.Genres)
            //    {
            //        path.Append(genre);
            //        path.Append(",");
            //    }
            //}
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path.ToString());
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var movie = JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                foreach (var movies in movie.results)
                {
                    var rate = _context.Rates.Where(n => n.MovieId == movies.Id);
                    var countRate = await rate.CountAsync();
                    movies.VoteAverage = (movies.VoteAverage * movies.VoteCount + await rate.SumAsync(n => n.Rate)) / (countRate + movies.VoteCount);
                    movies.VoteAverage = double.Parse(movies.VoteAverage.ToString(".000"));
                    movies.VoteCount += countRate;
                }
                return movie;
            }
            return null;
        }

        public async Task<GuestSession> CreateSession()
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/authentication/guest_session/new?{api_key}";

            
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var respons = JsonConvert.DeserializeObject<TMDbLib.Objects.Authentication.GuestSession>(strContent);
                return respons;
            }

            return null;

        }

        public async Task<List<Country>> Countries()
        {
            
            List<Country> AllCountries = await _cache.GetOrCreateAsync("Countries", async options =>
            {
                HttpClient httpClient = new HttpClient();


                string path = $"https://api.themoviedb.org/3/configuration/countries?{api_key}";

                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStreamAsync();

                    using Stream receiveStream = res;
                    using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    string strContent = readStream.ReadToEnd();

                    var countries = JsonConvert.DeserializeObject<List<Country>>(strContent);
                    options.SetOptions(_cacheOption);
                    options.SetValue(countries);
                    return countries;
                }
                return null;
            });
           
            return AllCountries;
        }

        


    }
}

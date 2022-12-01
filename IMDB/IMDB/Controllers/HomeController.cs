using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Infrastructure;
using IMDB.Domain.CardViewModel;
using IMDB.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using IMDB.DataLayer.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using IMDB.Services.Database;

namespace IMDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovie _movie;
        private readonly ReviewRepo _user;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOption;
        
        public HomeController(IMovie movie, ReviewRepo user, IMemoryCache cache)
        {
            _movie = movie;
            _user = user;
            _cache = cache;
            _cacheOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var temp = await _user.GetByMovieId(882598);
                
                HomeViewModel model = new HomeViewModel();

                model.PopularMovies = await _cache.GetOrCreateAsync("PopularMovies", async options =>
                {
                    options.SetOptions(_cacheOption);
                    var getmovies = await _movie.GetPopularMovies(1);
                    if(getmovies!=null)options.SetValue(getmovies);
                    return getmovies;
                });

                model.TopRatedMovies = await _cache.GetOrCreateAsync("TopRatedMovies", async options =>
                {
                    options.SetOptions(_cacheOption);
                    var getmovies = await _movie.GetTopRatedMovies(1);
                    if (getmovies != null) options.SetValue(getmovies);
                    return getmovies;
                });

                model.PopularPeople = await _cache.GetOrCreateAsync("PopularPeople", async options =>
                {
                    options.SetOptions(_cacheOption);
                    var getmovies = await _movie.GetPopularPeople();
                    if (getmovies != null) options.SetValue(getmovies);
                    return getmovies;
                });


                model.TrendingMoviesOfWeek = await _cache.GetOrCreateAsync("TrendingMoviesOfWeek", async options =>
                {
                    options.SetOptions(_cacheOption);
                    var getmovies = await _movie.GetTrendingMovies(TMDbLib.Objects.General.MediaType.Movie, TMDbLib.Objects.Trending.TimeWindow.Week);
                    if (getmovies != null) options.SetValue(getmovies);
                    return getmovies;
                });


                model.TrendingMoviesOfDay = await _cache.GetOrCreateAsync("TrendingMoviesOfDay", async options =>
                {
                    options.SetOptions(_cacheOption);
                    var getmovies = await _movie.GetTrendingMovies(TMDbLib.Objects.General.MediaType.Movie, TMDbLib.Objects.Trending.TimeWindow.Day);
                    if (getmovies != null) options.SetValue(getmovies);
                    return getmovies;
                });

                //model.PopularMovies = await _movie.GetPopularMovies(1);
                //model.TopRatedMovies = await _movie.GetTopRatedMovies(1);
                //model.PopularPeople = await _movie.GetPopularPeople();
                //model.TrendingMoviesOfWeek = await _movie.GetTrendingMovies(TMDbLib.Objects.General.MediaType.Movie, TMDbLib.Objects.Trending.TimeWindow.Week);
                //model.TrendingMoviesOfDay = await _movie.GetTrendingMovies(TMDbLib.Objects.General.MediaType.Movie, TMDbLib.Objects.Trending.TimeWindow.Day);
                return View(model);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                return View("NetError");
            }
        }
    }
}
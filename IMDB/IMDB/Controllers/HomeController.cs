using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TMDbLib.Objects.Movies;
using System.IO;
using System.Text;
using Infrastructure;
using IMDB.Domain.CardViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace IMDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovie _movie;
        private readonly IUser _user;
        public HomeController(IMovie movie, IUser user)
        {
            _movie = movie;
            _user = user;
        }

        public IActionResult Index()
        {
            try
            {
                HomeViewModel model = new HomeViewModel();
                model.PopularMovies = _movie.GetPopularMovies(1);
                model.TopRatedMovies = _movie.GetTopRatedMovies(1);
                model.LatestMovie = _movie.GetLatestMovies();
                model.PopularPeople = _movie.GetPopularPeople();
                model.TrendingMoviesOfWeek = _movie.GetTrendingMovies(TMDbLib.Objects.General.MediaType.Movie,TMDbLib.Objects.Trending.TimeWindow.Week);
                model.TrendingMoviesOfDay = _movie.GetTrendingMovies(TMDbLib.Objects.General.MediaType.Movie, TMDbLib.Objects.Trending.TimeWindow.Day);
        
                return View(model);
            }
            catch (System.AggregateException)
            {
                return View("NetError");
            }
            
        }

    }
}

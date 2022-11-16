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
                return View(model);
            }
            catch (System.AggregateException)
            {
                return View("NetError");
            }
            
        }

    }
}

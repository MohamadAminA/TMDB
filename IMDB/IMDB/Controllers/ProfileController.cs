using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IMDB.DataLayer.Model;
using IMDB.Domain.CardViewModel;
using IMDB.Services.Database;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        readonly IUser _user;
        private MovieListRepo _list { get;}
        private IMovie _movie { get; }
        
        public ProfileController(IUser user, MovieListRepo list,IMovie movie)
        {
            _user = user;
            _list = list;
            _movie = movie;
        }

        public async Task<IActionResult> Index()
        {
            if(!User.Identity.IsAuthenticated)
                return RedirectToActionPermanent("Index", "SignIn");
            int userId = int.Parse(User.Identity.Name);
            ProfileViewModel model = new ProfileViewModel();
            model.User = await _user.GetUserById(userId);
            var WatchList = await _list.GetWatchListById(userId);
            List<Movie> WatchListMovies = new List<Movie>();
            foreach (var movie in WatchList)
            {
                WatchListMovies.Add(await _movie.GetMovieById(movie.Id));
            }
            model.WatchList = WatchListMovies;
            model.MovieLists = await _list.GetMovieListsById(userId);
            return View(model);
        }
        [HttpPost]  
        public async Task<IActionResult> AddMovieList(string Title)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToActionPermanent("Index","SignIn");
            int userId = int.Parse(User.Identity.Name);
            await _list.AddFavouriteList(Title, userId);
            
            return RedirectToAction("Index");
        }
    }
}

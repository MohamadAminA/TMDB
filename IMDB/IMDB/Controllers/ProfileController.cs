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
                var MovieDetail = await _movie.GetMovieById(movie.MovieId);
                if(MovieDetail != null )
                    WatchListMovies.Add(MovieDetail);
            }
            model.WatchList = WatchListMovies;

            var MovieLists = await _list.GetMovieListsById(userId);
            model.MovieLists = new List<FavouriteMovieList>();
            foreach (var list in MovieLists)
            {
                var FavouriteList = new FavouriteMovieList()
                {
                    CreateDate = list.CreateDate,
                    Id = list.Id,
                    Title = list.Title,
                    User = list.User,
                    Movies = new List<Movie>()
                };
                foreach (var movie in list.FavouriteMovies)
                {
                    var MovieDetails = await _movie.GetMovieById(movie.MovieId);
                    if (MovieDetails != null)
                        FavouriteList.Movies.Add(MovieDetails);
                }
                model.MovieLists.Add(FavouriteList);
            }

            return View(model);
        }
        [HttpPost]  
        public async Task<IActionResult> AddMovieList(string Title)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToActionPermanent("Index","SignIn");
            int userId = int.Parse(User.Identity.Name);
            await _list.AddFavouriteList(Title, userId);
            await _list.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

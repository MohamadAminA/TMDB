using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IMDB.DataLayer.Model;
using IMDB.Domain.CardViewModel;
using IMDB.Services.Api;
using IMDB.Services.Database;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    public class MovieDetailsController : Controller
    {
        private readonly IMovie _movie;
        private readonly ReviewRepo _review;
        public MovieDetailsController(IMovie movie, ReviewRepo review)
        {
            _movie = movie;
            _review = review;
        }

        public async Task<IActionResult> Index(int id)
        {
            //await _movie.RateMovie(880, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(881, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(882, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(883, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(884, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(885, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(886, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(887, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(888, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(889, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(550, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(551, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(552, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(553, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(554, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(555, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(556, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(557, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(558, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(559, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //await _movie.RateMovie(560, 5.5, User.FindFirstValue(ClaimTypes.NameIdentifier));
            //var temp = await _movie.RatedMoviesBySession(User.FindFirstValue(ClaimTypes.NameIdentifier));
            MovieDetailsViewModel model = new MovieDetailsViewModel();
            model.Movie = await _movie.GetMovieById(id);
            model.Credits = await _movie.GetMovieCreditsById(id);
            
            model.Reviews = await _review.GetByMovieId(id);
            
            model.SimilarMovie = await _movie.SimilarMovies(id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetReview(MovieDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("ReviewContent", "Please LoginIn or SignUp First!");
                return View(model);
            }
            MovieReview review = new MovieReview()
            {
                MovieId = model.Movie.Id,
                Content = model.ReviewContent,
                UserId = Int32.Parse(User.Identity.Name),
                ReplyParent = model.ReplayParent
            };
            await _review.AddReview(review);
            await _review.SaveChanges();
            return RedirectToAction("Index", "MovieDetails",new { id = model.Movie.Id });
        }
    }
}

using IMDB.Domain.CardViewModel;
using IMDB.Services.Api;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    public class MovieDetailsController : Controller
    {
        private readonly IMovie _movie;
        public MovieDetailsController(IMovie movie)
        {
            _movie = movie;
        }

        public IActionResult Index(int id)
        {
            MovieDetailsViewModel model = new MovieDetailsViewModel();
            model.Movie = _movie.GetMovieById(id);
            model.Credits = _movie.GetMovieCreditsById(id);
            model.Reviews = _movie.GetReviewsOfMovieById(id);
            return View(model);
        }
    }
}

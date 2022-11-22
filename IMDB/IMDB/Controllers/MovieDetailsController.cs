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
            model.SimilarMovie = _movie.SimilarMovies(id);
            model.trailers = _movie.GetVideoById(id);
            return View(model);
        }
    }
}

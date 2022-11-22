using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Index(int id)
        {
            MovieDetailsViewModel model = new MovieDetailsViewModel();
            model.Movie = await _movie.GetMovieById(id);
            model.Credits = await _movie.GetMovieCreditsById(id);
            model.Reviews = await _movie.GetReviewsOfMovieById(id);
            model.SimilarMovie = await _movie.SimilarMovies(id);
            model.Movie.Key = (await _movie.GetVideoById(id)).Results[0]?.Key;
            foreach(var input in model.SimilarMovie.results){
                input.Key = (await _movie.GetVideoById(input.Id)).Results[0]?.Key;
            }
            return View(model);
        }
    }
}

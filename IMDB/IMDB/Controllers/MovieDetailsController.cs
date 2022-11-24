using System;
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
            model.SimilarMovie = await _movie.SimilarMovies(id);
           
            return View(model);
        }
    }
}

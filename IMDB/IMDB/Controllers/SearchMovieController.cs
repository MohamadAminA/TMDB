using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    public class SearchMovieController : Controller
    {
        IMovie _movie;
        public SearchMovieController(IMovie movie)
        {
            _movie = movie;
        }
        [HttpPost]
        public IActionResult Index(string searchText)
        {
            var model = _movie.SearchMovies(searchText,0);
            return View(model);
        }
    }
}

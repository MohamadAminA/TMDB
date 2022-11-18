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

        public IActionResult Index(string searchText,int page = 1,string searchType = "Movies")
        {
            var model = _movie.SearchMovies(searchText, page);
            return View(model);
        }
    }
}

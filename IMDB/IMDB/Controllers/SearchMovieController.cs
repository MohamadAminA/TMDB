using System.Threading.Tasks;
using IMDB.Domain.CardViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Controllers
{
    public class SearchMovieController : Controller
    {
        IMovie _movie;
        public SearchMovieController(IMovie movie)
        {
            _movie = movie;
        }
        public async Task<IActionResult> Index(DiscoverFilterMovie filter)
        {
            var model = new SearchViewModel();
            model.Movies = await _movie.DiscoverMovie(filter);
            model.Filter = filter;
            return View(model);
        }
        [HttpPost("Search")]
        public async Task<IActionResult> Search(string query,int date)
        {
            var model = new SearchViewModel();
            model.Movies = await _movie.SearchMovies(query,date);
            model.Filter = new DiscoverFilterMovie();
            return View("Index",model);
        }
    }
}

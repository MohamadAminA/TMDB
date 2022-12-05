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
      
        public async Task<IActionResult> Search(string query,int date, DiscoverFilterMovie filter, int page = 1)
        {
            var model = new SearchViewModel();

            if (!string.IsNullOrWhiteSpace(query))
            {
                var search = new SearchMovie()
                {
                    ReleaseDate = date,
                    SearchTitle = query
                };
                model.Search = search;
                model.Movies = await _movie.SearchMovies(query, date, page);
            }
            else
            {
                filter.Page = page;
                model.Movies = await _movie.DiscoverMovie(filter);
                model.Filter = filter;
                model.Search = new SearchMovie();
            }
            return View("Index",model);
        }
    }
}

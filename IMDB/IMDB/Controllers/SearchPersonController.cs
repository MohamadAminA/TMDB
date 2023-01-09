using System.Threading.Tasks;
using IMDB.Domain.CardViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
	public class SearchPersonController : Controller
	{
        IMovie _movie;
        public SearchPersonController(IMovie movie)
        {
            _movie = movie;
        }
        public async Task<IActionResult> Index(string query,int page = 1)
		{
            SearchPeopleViewModel model = new SearchPeopleViewModel();
            model.People = await _movie.SearchPeople(query, page);
            model.Query = query;
            model.Page = page;
			return View(model);
		}
	}
}

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
        public IActionResult Index(string query)
		{
            
			return View();
		}
	}
}

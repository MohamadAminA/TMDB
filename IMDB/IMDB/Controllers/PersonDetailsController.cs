using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    public class PersonDetailsController : Controller
    {
        private IMovie _movie { get;}
        public PersonDetailsController(IMovie movie)
        {
            _movie = movie;
        }

        public async Task<IActionResult> Index(int id)
        {
            var details = await _movie.GetPersonDetailes(id);
            details.MovieCredits = await _movie.GetCreditsByPersonId(id);
            return View(details);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    public class SupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

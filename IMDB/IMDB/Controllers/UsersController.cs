using System.Threading.Tasks;
using IMDB.Services.Database;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    public class UsersController : Controller
    {
        readonly IUser _user;
        public UsersController(IUser user)
        {
            _user = user;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _user.GetUsersAsync());
        }
    }
}

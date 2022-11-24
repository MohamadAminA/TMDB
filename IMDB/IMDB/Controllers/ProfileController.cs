using System;
using IMDB.DataLayer.Model;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        readonly IUser _user;
        public ProfileController(IUser user)
        {
            _user = user;
        }
        
        public IActionResult Index()
        {
            User user = _user.GetUserById(Int32.Parse(User.Identity.Name));
            return View(user);
        }
    }
}

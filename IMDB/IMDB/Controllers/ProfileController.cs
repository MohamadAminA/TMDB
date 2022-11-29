using System;
using IMDB.DataLayer.Model;
using IMDB.Domain.CardViewModel;
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
            ProfileViewModel model = new ProfileViewModel();
            model.User = _user.GetUserById(Int32.Parse(User.Identity.Name));
            model.WatchList = new System.Collections.Generic.List<Domain.DTOs.MovieDTO.Movie>();
            return View(model);
        }
    }
}

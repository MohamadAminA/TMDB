using System;
using IMDB.DataLayer.Model;
using IMDB.Domain.CardViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
	public class SignUpController : Controller
	{
		private readonly IUser _user;
		public SignUpController(IUser user)
		{
			_user = user;
		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
        public IActionResult Index(SignUpViewModel model)
        {
			if (!ModelState.IsValid)
				return View(model);
			if (_user.IsUserExist(name: model.UserName))
			{
				ModelState.AddModelError("password", "Username is already registered");
				return View(model);
			}
			_user.AddUser(new User()
			{
				Name = model.UserName,
				Password = model.Password,
				RegisterDate = DateTime.Now
			}) ;
            return RedirectToAction("Index","SignIn");
        }

    }
}

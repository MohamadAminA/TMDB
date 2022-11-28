using System;
using System.Threading.Tasks;
using IMDB.DataLayer.Model;
using IMDB.Domain.CardViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
	public class SignUpController : Controller
	{
		private readonly IUser _user;
        private readonly IMovie _movie;
        public SignUpController(IUser user, IMovie movie)
		{
			_user = user;
			_movie = movie;
		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> Index(SignUpViewModel model)
        {
			if (!ModelState.IsValid)
				return View(model);
			if (_user.IsUserExist(name: model.UserName))
			{
				ModelState.AddModelError("password", "Username is already registered");
				return View(model);
			}
			var session = await _movie.CreateSession(await _movie.TemporaryRequestToken());
			_user.AddUser(new User()
			{
				Name = model.UserName,
				Password = model.Password,
				RegisterDate = DateTime.Now,
				Session = session
			}) ;
            return RedirectToAction("Index","SignIn");
        }

    }
}

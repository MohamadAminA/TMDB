using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TMDbLib.Objects.Movies;
using System.IO;
using System.Text;
using Infrastructure;
using IMDB.Domain.CardViewModel;
using Microsoft.AspNetCore.Http;

namespace IMDB.Controllers
{
    public class signinController: Controller
    {
        private readonly IMovie _movie;
        private readonly IUser _user;
        public signinController(IMovie movie, IUser user)
        {
            _movie = movie;
            _user = user;
        }
        [HttpPost]
        public IActionResult signinUser(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Signin",model);
            var user = _user.GetUserByName(model.UserName);
            if (model.Password== user.Password)
            {
                if (string.IsNullOrEmpty(this.Request.Cookies["SessionId"]))
                {
                    var newSession = _movie.CreateSession();
                    Response.Cookies.Append("SessionId", newSession.GuestSessionId, new CookieOptions
                    {
                        HttpOnly = true,
                        Path = Request.PathBase.HasValue ? this.Request.PathBase.ToString() : "/",
                        Secure = Request.IsHttps,
                        Expires = newSession.ExpiresAt
                    });

                }
            }
            else
            {
                ModelState.AddModelError("Password","UserName or Password is not correct");
                return View("Signin", model);
            }
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using TMDbLib.Objects.Movies;

namespace IMDB.Controllers
{
    public class signinController : Controller
    {
        private readonly IMovie _movie;
        public signinController(IMovie movie)
        {
            _movie = movie;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult signinUser()
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
            return Ok();
        }
    }
}

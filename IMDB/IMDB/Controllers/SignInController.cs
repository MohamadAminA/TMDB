using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using IMDB.Domain.CardViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace IMDB.Controllers
{
    public class SignInController : Controller
    {
        private readonly IMovie _movie;
        private readonly IUser _user;
        public SignInController(IMovie movie, IUser user)
        {
            _movie = movie;
            _user = user;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = _user.GetUserByName(model.UserName);
            if (user != null && !string.IsNullOrWhiteSpace(user.Password) && model.Password == user.Password)
            {
                UserAuthentication(user.Id, user.Name,user.Session, model.RememberMe);
            }
            else
            {
                ModelState.AddModelError("Password", "نام کاربری یا رمز اشتباه می باشد");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
        private List<Claim> UserAuthentication(int id, string userName,string session, bool isRememberMe = false)
        {
            var claims = new List<Claim>()
            {
                new System.Security.Claims.Claim(ClaimTypes.Name,id.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.GivenName,userName),
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier,session)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = isRememberMe
            };
            HttpContext.SignInAsync(principle, properties);
            return claims;
        }

        #region Logout
        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
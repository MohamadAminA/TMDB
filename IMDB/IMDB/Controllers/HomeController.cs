﻿using System;
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
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace IMDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovie _movie;
        private readonly IUser _user;
        public HomeController(IMovie movie, IUser user)
        {
            _movie = movie;
            _user = user;
        }

        public IActionResult Index()
        {
            try
            {
                HomeViewModel model = new HomeViewModel();
                model.PopularMovies = _movie.GetPopularMovies(1);
                model.TopRatedMovies = _movie.GetTopRatedMovies(1);
                model.LatestMovie = _movie.GetLatestMovies();
                model.PopularPeople = _movie.GetPopularPeople();
                model.TrendingMoviesOfWeek = _movie.GetTrendingMovies(TMDbLib.Objects.General.MediaType.Movie,TMDbLib.Objects.Trending.TimeWindow.Week);
                model.TrendingMoviesOfDay = _movie.GetTrendingMovies(TMDbLib.Objects.General.MediaType.Movie, TMDbLib.Objects.Trending.TimeWindow.Day);
        
                return View( model);
            }
            catch (System.AggregateException)
            {
                return View();
            }
            
        }
            [HttpPost]
            public IActionResult loginfunc(SignInViewModel model)
            {
                if (!ModelState.IsValid)
                    return View(model);
                var user = _user.GetUserByName(model.UserName);
                if (user != null && !string.IsNullOrWhiteSpace(user.Password) && model.Password == user.Password)
                {
                    #region Set Cookie for Session
                    if (string.IsNullOrEmpty(this.Request.Cookies["SessionId"]))
                    {
                        var newSession = _movie.CreateSession();
                        Response.Cookies.Append("SessionId", newSession.GuestSessionId, new CookieOptions
                        {
                            HttpOnly = false,
                            Path = Request.PathBase.HasValue ? this.Request.PathBase.ToString() : "/",
                            Secure = Request.IsHttps,
                            Expires = newSession.ExpiresAt
                        });
                    }
                    #endregion
                    UserAuthentication(user.Id, user.Name, model.RememberMe);
                }
                else
                {
                    ModelState.AddModelError("Password", "The username or password is incorrect");
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
            }
            private List<Claim> UserAuthentication(int id, string userName, bool isRememberMe = false)
            {
                var claims = new List<Claim>()
            {
                new System.Security.Claims.Claim(ClaimTypes.Name,id.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.GivenName,userName)
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
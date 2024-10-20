﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IMDB.DataLayer.Model;
using IMDB.Domain.CardViewModel;
using IMDB.Services.Api;
using IMDB.Services.Database;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    public class MovieDetailsController : Controller
    {
        private readonly IMovie _movie;
        private readonly ReviewRepo _review;
        public MovieDetailsController(IMovie movie, ReviewRepo review)
        {
            _movie = movie;
            _review = review;
        }

        public async Task<IActionResult> Index(int id)
        {

            MovieDetailsViewModel model = new MovieDetailsViewModel();
            model.Movie = await _movie.GetMovieById(id);
            if(model.Movie == null)
                return RedirectToAction("Error", "Home");
            model.Credits = await _movie.GetMovieCreditsById(id);
            
            model.Reviews = await _review.GetByMovieId(id);
            
            model.SimilarMovie = await _movie.SimilarMovies(id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetReview(MovieDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("ReviewContent", "Please LoginIn or SignUp First!");
                return View(model);
            }
            MovieReview review = new MovieReview()
            {
                MovieId = model.Movie.Id,
                Content = model.ReviewContent,
                UserId = Int32.Parse(User.Identity.Name),
                ReplayParent = model.ReplayParent
            };
            await _review.AddReview(review);
            await _review.SaveChanges();
            return RedirectToAction("Index", "MovieDetails",new { id = model.Movie.Id });
        }
    }
}

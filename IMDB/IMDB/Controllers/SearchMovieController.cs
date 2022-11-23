﻿using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    public class SearchMovieController : Controller
    {
        IMovie _movie;
        public SearchMovieController(IMovie movie)
        {
            _movie = movie;
        }

        public IActionResult Index(string searchText,int releasDate,int page = 1)
        {
            var model = _movie.SearchMovies(searchText,releasDate, page);
            return View(model);
        }
    }
}

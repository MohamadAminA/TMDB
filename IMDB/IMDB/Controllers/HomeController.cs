using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Infrastructure;
using IMDB.Domain.CardViewModel;
using IMDB.Domain.DTOs;

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

        public async Task<IActionResult> Index()
        {
            try
            {
                HomeViewModel model = new HomeViewModel();

                model.PopularMovies = await _movie.GetPopularMovies(1);
                model.TopRatedMovies = await _movie.GetTopRatedMovies(1);
                model.LatestMovie = await _movie.GetLatestMovies();
                model.PopularPeople = await _movie.GetPopularPeople();
                model.TrendingMoviesOfWeek = await _movie.GetTrendingMovies(TMDbLib.Objects.General.MediaType.Movie, TMDbLib.Objects.Trending.TimeWindow.Week);
                model.TrendingMoviesOfDay = await _movie.GetTrendingMovies(TMDbLib.Objects.General.MediaType.Movie, TMDbLib.Objects.Trending.TimeWindow.Day);
                foreach (MovieDTO.Movie movie in model.PopularMovies.results)
                {
                    movie.Key = (await _movie.GetVideoById(movie.Id)).Results.LastOrDefault()?.Key;
                }
                //foreach (MovieDTO.Movie movie in model.TopRatedMovies.results)
                //{
                //    movie.Key = (await _movie.GetVideoById(movie.Id)).Results.FirstOrDefault()?.Key;
                //}

                //model.LatestMovie.Key = (await _movie.GetVideoById(model.LatestMovie.Id)).Results.FirstOrDefault()?.Key;

                //foreach (MovieDTO.Movie movie in model.TrendingMoviesOfWeek.results)
                //{
                //    movie.Key = (await _movie.GetVideoById(movie.Id)).Results.FirstOrDefault()?.Key;
                //}
                //foreach (MovieDTO.Movie movie in model.TrendingMoviesOfDay.results)
                //{
                //    movie.Key = (await _movie.GetVideoById(movie.Id)).Results.FirstOrDefault()?.Key;
                //}
                return View(model);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                return View("NetError");
            }
        }
    }
}
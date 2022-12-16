using IMDB.DataLayer.Model;
using IMDB.Services.Database;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IMovie _movie;
        public MovieListRepo _list { get; set; }

        public ApiController(IMovie movie, MovieListRepo list)
        {
            _movie = movie;
            _list = list;
        }

        [HttpGet("GetOneVideoKey")]
        public IActionResult GetOneVideoKey(int id)
        {
            var sendPath = "";
            foreach (var path in (_movie.GetVideoById(id).Result).Results)
            {
                sendPath += path.Key + '*';
            }
            return Ok(sendPath);
            //  return sendPath ;
            // return Ok("https://www.youtube.com/embed/"+(_movie.GetVideoById(id).Result).Results.LastOrDefault()?.Key);
        }
        [HttpGet("Getone")]
        public IActionResult Getone(String id)
        {
            String[] Array = id.Split('*');

            return Ok((_movie.GetVideoById(int.Parse(Array[0])).Result).Results[int.Parse(Array[1])]?.Key);

        }
        #region MovieList
        [HttpGet("AddMovieToList")]
        public async Task<IActionResult> AddMovieToList(string listId, string movieId)
        {
            await _list.AddMovieToList(int.Parse(listId), int.Parse(movieId));
            await _list.SaveChangesAsync();
            return Ok();

        }
        [HttpGet("RemoveMovieFromList")]
        public async Task<IActionResult> RemoveMovieFromList(string listId, string movieId)
        {
            await _list.RemoveMovieFromList( int.Parse(movieId), int.Parse(listId));
            await _list.SaveChangesAsync();
            return Ok();

        }
        [HttpGet("AddList")]
        public async Task<IActionResult> AddList(string Title)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToActionPermanent("Index", "SignIn");
            await _list.AddFavouriteList(Title, int.Parse(User.Identity.Name));
            await _list.SaveChangesAsync();
            return Ok();

        }
        [HttpGet("RemovieList")]
        public async Task<IActionResult> RemovieList(string ListId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToActionPermanent("Index", "SignIn");
            await _list.RemoveList(int.Parse(ListId));
            await _list.SaveChangesAsync();
            return Ok();

        }

        [HttpGet("GetLists")]
        public async Task<List<FavouriteList>> GetLists()
        {
            if (!User.Identity.IsAuthenticated)
                return null;
            var result = await _list.GetMovieListsById(int.Parse(User.Identity.Name));
            return result;
        }
        #endregion


        #region WatchList
        [HttpGet("AddMovieToWatchList")]
        public async Task<IActionResult> AddMovieToWatchList(string movieId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToActionPermanent("Index", "SignIn");
            await _list.AddToWatchList(new DataLayer.Model.WatchList()
            {
                MovieId = int.Parse(movieId),
                UserId = int.Parse(User.Identity.Name)
            });
            await _list.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("RemoveMovieFromWatchList")]
        public async Task<IActionResult> RemoveMovieFromWatchList(string movieId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToActionPermanent("Index", "SignIn");
            await _list.RemoveFromWatchList(int.Parse(User.Identity.Name),int.Parse(movieId));
            await _list.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Rate
        [HttpGet("RateMovie")]
        public async Task<IActionResult> RateMovie(string movieId,int rate)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToActionPermanent("Index", "SignIn");
            await _list.AddRateMovie(int.Parse(User.Identity.Name), int.Parse(movieId),rate);
            await _list.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("RemoveRate")]
        public async Task<IActionResult> RemoveRate(string movieId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToActionPermanent("Index", "SignIn");
            await _list.RemoveRateMovie(int.Parse(User.Identity.Name), int.Parse(movieId));
            await _list.SaveChangesAsync();
            return Ok();
        }
        #endregion
    }
}
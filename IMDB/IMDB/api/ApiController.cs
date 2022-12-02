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

        [HttpGet("AddMovieToList")]
        public async Task<IActionResult> AddMovieToList(string listId,string movieId)
        {
            await _list.AddMovieToList(int.Parse(listId),int.Parse(movieId));
            return Ok();

        }
        [HttpGet("AddMovieToWatchList")]
        public async Task<IActionResult> AddMovieToWatchList(string movieId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToActionPermanent("Index","SignIn");
            await _list.AddToWatchList(new DataLayer.Model.WatchList()
            {
                MovieId = int.Parse(movieId),
                UserId = int.Parse(User.Identity.Name)
            });
            return Ok();

        }

    }
}
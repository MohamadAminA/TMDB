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
        public ApiController(IMovie movie)
        {
            _movie = movie;
        }
        [HttpGet("GetOneVideoKey")]
        public IActionResult GetOneVideoKey(int id)
        {
            return Ok("https://www.youtube.com/watch?v=" + (_movie.GetVideoById(id).Result).Results.LastOrDefault()?.Key);
        }
    }
}

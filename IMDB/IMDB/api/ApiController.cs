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
            var sendPath = "";
            foreach (var path in (_movie.GetVideoById(id).Result).Results)
            {
                sendPath += path.Key + ",";
            }
            return Ok(sendPath);
            //  return sendPath ;
            // return Ok("https://www.youtube.com/embed/"+(_movie.GetVideoById(id).Result).Results.LastOrDefault()?.Key);
        }
        [HttpGet("Getone")]
        public IActionResult Getone(String id)
        {
            String[] Array = id.Split(',');

            return Ok((_movie.GetVideoById(int.Parse(Array[0])).Result).Results[int.Parse(Array[1])]?.Key);

        }


    }
}
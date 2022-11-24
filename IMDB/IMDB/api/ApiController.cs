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
        public async Task<IActionResult> getOneVideoKey(int id)
        {
            return Ok((await _movie.GetVideoById(id)).Results.LastOrDefault().Key);
        }
    }
}

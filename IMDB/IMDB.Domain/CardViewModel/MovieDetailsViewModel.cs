using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Reviews;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Domain.CardViewModel
{
    public class MovieDetailsViewModel
    {
        public Movie Movie{ get; set; }
        public Credits Credits{ get; set; }
        public APIListResult<Review> Reviews{ get; set; }
    }
}

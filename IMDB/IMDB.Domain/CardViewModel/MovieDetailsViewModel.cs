using TMDbLib.Objects.Reviews;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Domain.CardViewModel
{
    public class MovieDetailsViewModel
    {
        public Movie Movie{ get; set; }
        public TMDbLib.Objects.Movies.Credits Credits { get; set; }
        public APIListResult<Review> Reviews{ get; set; }
        public APIListResult<Movie> SimilarMovie { get; set; }

    }
}

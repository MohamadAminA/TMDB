using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Domain.CardViewModel
{
    public class HomeViewModel
    {
        public APIListResult<Movie> PopularMovies { get; set; }
        public APIListResult<Movie> TopRatedMovies { get; set; }
        public Movie LatestMovie { get; set; }
        public APIListResult<Person> PopularPeople { get; set; }
        public APIListResult<Movie> TrendingMoviesOfWeek { get; set; }
        public APIListResult<Movie> TrendingMoviesOfDay { get; set; }
        public APIListResult<Movie> TrendingEpisodeOfDay { get; set; }
        public APIListResult<Movie> TrendingEpisodeOfWeek { get; set; }

        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string UserName { get; set; }
        [DisplayName("رمز")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Password { get; set; }
        [DisplayName("مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}

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

        [DisplayName("User Name")]
        [Required(ErrorMessage = "Please enter Enter the {0} ")]
        public string UserName { get; set; }
        [DisplayName("Passwoed")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Enter the {0} ")]
        public string Password { get; set; }
        [DisplayName("remember me")]
        public bool RememberMe { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.People;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Domain.CardViewModel
{
    public class HomeViewModel
    {
        public APIListResult<Movie> PopularMovies { get; set; }
        public APIListResult<Movie> TopRatedMovies { get; set; }
        public APIListResult<Person> PopularPeople { get; set; }
        public APIListResult<Person> BornTodayPeople { get; set; }
        public APIListResult<Movie> TrendingMoviesOfWeek { get; set; }
        public APIListResult<Movie> TrendingMoviesOfDay { get; set; }

    }
}

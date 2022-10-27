using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;

namespace Infrastructure
{
    public interface IMovie
    {
        public List<Movie> GetPopularMovies();
        public Movie GetMovieById(int id);
    }
}

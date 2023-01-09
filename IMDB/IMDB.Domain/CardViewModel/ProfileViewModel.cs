using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB.DataLayer.Model;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Domain.CardViewModel
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public List<Movie> WatchList { get; set; }
        public List<FavouriteMovieList> MovieLists { get; set; }
    }
}

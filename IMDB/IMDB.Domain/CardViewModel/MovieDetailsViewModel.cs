using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.TvShows;
using static IMDB.Domain.DTOs.MovieDTO;
using IMDB.DataLayer.Model;

namespace IMDB.Domain.CardViewModel
{
    public class MovieDetailsViewModel
    {
        public Movie Movie{ get; set; }
        public TMDbLib.Objects.Movies.Credits Credits { get; set; }
        public List<MovieReview> Reviews{ get; set; }
        public APIListResult<Movie> SimilarMovie { get; set; }

        #region Review
        //public string ReviewName { get; set; }
        //public string ReviewEmail { get; set; }
        //public string ReviewPhone { get; set; }
        [Required]
        public string ReviewContent { get; set; }
        public int? ReplayParent { get; set; }
        #endregion
    }
}

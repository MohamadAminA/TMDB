using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Domain.CardViewModel
{
    public class SearchViewModel
    {
        public APIListResult<Movie> Movies { get; set; }
        public DiscoverFilterMovie Filter { get; set; }

    }
}

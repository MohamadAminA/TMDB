using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;

namespace IMDB.Domain.DTOs
{
    public class MovieDTO
    {
        
        public class APIListResult<T>
        {
            public int Page { get; set; }
            public List<T> results { get; set; }
            public int total_pages { get; set; }
            public int total_results { get; set; }
        }

    }
}

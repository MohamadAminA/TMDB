﻿using System;
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
        public SearchMovie Search { get; set; }
    }
    public class SearchPeopleViewModel
    {
        public Domain.DTOs.MovieDTO.APIListResult<TMDbLib.Objects.People.Person> People { get; set; }
        public int Page { get; set; }
        public string Query { get; set; }
    }

    public class SearchMovie
    {
        public string SearchTitle { get; set; }
        public int ReleaseDate { get; set; }
    }


}

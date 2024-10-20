﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB.Services.Api;

namespace DatabaseTSV
{
    public class DatasetMoviePosterPath
    {
        public static void SetPoster()
        {
            MovieRepo _movie = new MovieRepo();
            ReadWriteTSV tsv = new ReadWriteTSV();
            var values = tsv.Read("title.basic.1-200");
       //     values[0].Add("poster_path");
            values[0].Add("movie_key");
            //foreach (var record in values.Skip(1))
            //{
            //       record.Add("http://image.tmdb.org/t/p/original" + _movie.SearchMovies(record[3]).Result.results.FirstOrDefault().PosterPath);
       //     values[1].Add("http://image.tmdb.org/t/p/original" + _movie.SearchMovies(values[1][3]).Result.results.FirstOrDefault().PosterPath);
            values[1].Add("https://www.youtube.com/embed/" + _movie.GetVideoById(_movie.SearchMovies(values[1][3],int.Parse(values[1][5])).Result.results.FirstOrDefault().Id).Result.Results.FirstOrDefault().Key);
            //}
            tsv.Write(values);
        }

        public static void SetCreditProfile()
        {
            MovieRepo _movie = new MovieRepo();
            ReadWriteTSV tsv = new ReadWriteTSV();
            var values = tsv.Read("name.basics1-200");
            values[0].Add("poster_path");
            foreach (var record in values.Skip(1))
            {
                record.Add("http://image.tmdb.org/t/p/original" + _movie.SearchPersons(record[1]).Result.results.FirstOrDefault()?.ProfilePath);
            }
            tsv.Write(values);
        }
    }
}

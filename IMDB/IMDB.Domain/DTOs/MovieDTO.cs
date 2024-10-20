﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Changes;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;
using Newtonsoft.Json;
using TMDbLib.Objects.Discover;
using System.Diagnostics.CodeAnalysis;

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

        public class DiscoverFilterMovie
        {
            public int Page { get; set; } = 1;
            public string Region { get; set; }
            public DiscoverMovieSortBy? SortBy { get; set; }
            public bool IncludeAdult { get; set; }
            public string ReleaseDateFrom { get; set; }
            public string ReleaseDateTo { get; set; }
            public ReleaseDateType? ReleaseType { get; set; }
            public int? VoteCountFrom { get; set; }
            public int? VoteCountTo { get; set; }
            public double? VoteAverageFrom { get; set; }
            public double? VoteAverageTo { get; set; }
            public string People { get; set; }
            public string Genres { get; set; }
            public int? TimeFrom { get; set; }
            public int? TimeTo { get; set; }

        }

        public class Movie
        {
            [JsonProperty("account_states")]
            public AccountState AccountStates { get; set; }

            [JsonProperty("adult")]
            public bool Adult { get; set; }

            [JsonProperty("alternative_titles")]
            public AlternativeTitles AlternativeTitles { get; set; }

            [JsonProperty("backdrop_path")]
            public string BackdropPath { get; set; }

            [JsonProperty("belongs_to_collection")]
            public SearchCollection BelongsToCollection { get; set; }

            [JsonProperty("budget")]
            public long Budget { get; set; }

            [JsonProperty("changes")]
            public ChangesContainer Changes { get; set; }

            [JsonProperty("credits")]
            public Credits Credits { get; set; }

            [JsonProperty("genres")]
            public List<Genre> Genres { get; set; }

            [JsonProperty("homepage")]
            public string Homepage { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("images")]
            public Images Images { get; set; }

            [JsonProperty("imdb_id")]
            public string ImdbId { get; set; }

            [JsonProperty("keywords")]
            public KeywordsContainer Keywords { get; set; }

            [JsonProperty("lists")]
            public SearchContainer<ListResult> Lists { get; set; }

            [JsonProperty("original_language")]
            public string OriginalLanguage { get; set; }

            [JsonProperty("original_title")]
            public string OriginalTitle { get; set; }

            [JsonProperty("overview")]
            public string Overview { get; set; }

            [JsonProperty("popularity")]
            public double Popularity { get; set; }

            [JsonProperty("poster_path")]
            public string PosterPath { get; set; }

            [JsonProperty("production_companies")]
            public List<ProductionCompany> ProductionCompanies { get; set; }

            [JsonProperty("production_countries")]
            public List<ProductionCountry> ProductionCountries { get; set; }

            [JsonProperty("release_date")]
            public DateTime? ReleaseDate { get; set; }

            [JsonProperty("release_dates")]
            public ResultContainer<ReleaseDatesContainer> ReleaseDates { get; set; }

            [JsonProperty("external_ids")]
            public ExternalIdsMovie ExternalIds { get; set; }

            [JsonProperty("releases")]
            public Releases Releases { get; set; }

            [JsonProperty("revenue")]
            public long Revenue { get; set; }

            [JsonProperty("reviews")]
            public SearchContainer<ReviewBase> Reviews { get; set; }

            [JsonProperty("runtime")]
            public int? Runtime { get; set; }

            [JsonProperty("similar")]
            public SearchContainer<SearchMovie> Similar { get; set; }

            [JsonProperty("recommendations")]
            public SearchContainer<SearchMovie> Recommendations { get; set; }

            [JsonProperty("spoken_languages")]
            public List<SpokenLanguage> SpokenLanguages { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("tagline")]
            public string Tagline { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("translations")]
            public TranslationsContainer Translations { get; set; }

            [JsonProperty("video")]
            public bool Video { get; set; }

            [JsonProperty("videos")]
            public ResultContainer<Video> Videos { get; set; }

            [JsonProperty("watch/providers")]
            public SingleResultContainer<Dictionary<string, WatchProviders>> WatchProviders { get; set; }

            [JsonProperty("vote_average")]
            public double VoteAverage { get; set; }

            [JsonProperty("vote_count")]
            public int VoteCount { get; set; }

            [JsonProperty("Key")]
            public string Key { get; set; }

        }


    }

    public class CrewEqualityComparer : IEqualityComparer<Crew>
    {
        public bool Equals(Crew x, Crew y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode([DisallowNull] Crew obj)
        {
            return obj.Name.GetHashCode();
        }
    }
    public class CastEqualityComparer : IEqualityComparer<Cast>
    {
        public bool Equals(Cast x, Cast y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode([DisallowNull] Cast obj)
        {
            return obj.Name.GetHashCode();
        }
    }

}

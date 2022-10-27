using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Newtonsoft.Json;
using TMDbLib.Objects;
using TMDbLib.Objects.Authentication;
using TMDbLib.Objects.Movies;

namespace IMDB.Services.Api
{
    public class MovieRepo:IMovie
    {
        private const string api_key = "api_key=646f26e9bfd4f042f28a7160726dd239";
        
        public Movie GetMovieById(int id)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/{id}?{api_key}";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //httpClient.DefaultRequestHeaders.Add("x-api-key",
            //    token);

            HttpResponseMessage response = httpClient.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStreamAsync().Result;

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                Movie movie = Newtonsoft.Json.JsonConvert.DeserializeObject<Movie>(strContent);
            }
            return null;
        }

        public List<Movie> GetPopularMovies()
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/popular?{api_key}&language=en-US&page=1";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //httpClient.DefaultRequestHeaders.Add("x-api-key",
            //    token);

            HttpResponseMessage response = httpClient.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStreamAsync().Result;

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                List<Movie> popMovies = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Movie>>(strContent);
            return popMovies;
            }
            return null;
        }

        //        public void test()
        //        {
        //            var client = new HttpClient();

        //            var requestData = new Dictionary<string, string>
        //{
        //    {
        //  "value", "8.5"
        //}
        //};

        //            var request = new HttpRequestMessage()
        //            {
        //                RequestUri = new Uri($"https://api.themoviedb.org/3/authentication/token/new?api_key={api_key}"),
        //                Method = HttpMethod.Get,
        //                Content = new FormUrlEncodedContent(requestData)
        //            };
        //            //request.Headers.Add("Token", ""); // Add or modify headers

        //            var response = client.SendAsync(request);

        //            // To read the response as string
        //            var responseString = response.Result.Content.ReadAsStringAsync().Result;

        //            // To read the response as json
        //            //var responseJson = response.Result.Content.ReadAsStringAsync<ResponseObject>().Result;
        //        }
    }
}

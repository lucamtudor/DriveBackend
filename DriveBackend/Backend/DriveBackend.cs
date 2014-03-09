using System;
using System.Collections.Generic;
using System.Net;
using DriveBackend.Models;
using Newtonsoft.Json.Linq;

namespace DriveBackend.Backend
{
    internal class DriveBackend : IBackend
    {
        private string DriveUrl { get; set; }

        public DriveBackend(string driveUrl)
        {
            DriveUrl = driveUrl;
        }

        public List<Movie> ReadAllMovies()
        {
            List<Movie> movies = new List<Movie>();

            JArray movieEntries = GetMovieEntries();
            foreach (JObject entry in movieEntries)
            {
                Movie movie = ParseMovieEntry(entry);
                movies.Add(movie);
            }
            return movies;
        }

        private JArray GetMovieEntries()
        {
            WebClient webClient = new WebClient();
            webClient.Headers["Accept"] = "application/json";
            JObject response = JObject.Parse(webClient.DownloadString(new Uri(DriveUrl)));
            JArray movieEntries = (JArray) response["feed"]["entry"];
            return movieEntries;
        }

        private static Movie ParseMovieEntry(JToken entry)
        {
            Movie movie = new Movie();
            movie.Name = (string) entry["gsx$film"]["$t"];
            movie.LaunchDate = (string) entry["gsx$data"]["$t"];
            return movie;
        }

        public void CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
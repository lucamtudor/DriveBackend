using System.Collections.Generic;
using System.IO;
using DriveBackend.Models;
using Newtonsoft.Json;

namespace DriveBackend.Backend
{
    internal class FileBackend : IBackend
    {
        public FileBackend(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; set; }

        public List<Movie> ReadAllMovies()
        {
            StreamReader fileReader = new StreamReader(FileName);

            List<Movie> movieList = new List<Movie>();
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                Movie movie = JsonConvert.DeserializeObject<Movie>(line);
                movieList.Add(movie);
            }
            return movieList;
        }

        public void CreateMovie(Movie movie)
        {
            StreamWriter fileWriter = new StreamWriter(FileName, true);
            string movieSerialization = JsonConvert.SerializeObject(movie);
            fileWriter.WriteLine(movieSerialization);
            fileWriter.Close();
        }
    }
}
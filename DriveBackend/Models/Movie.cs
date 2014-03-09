using System;

namespace DriveBackend.Models
{
    internal class Movie
    {
        public Movie()
        {
        }

        public Movie(string name, string launchDate)
        {
            Name = name;
            LaunchDate = launchDate;
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Launch Date: {1}", Name, LaunchDate);
        }

        public string Name { get; set; }
        public string LaunchDate { get; set; }
    }
}
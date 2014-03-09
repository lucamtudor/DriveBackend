using System;
using System.Collections;
using System.Collections.Generic;
using DriveBackend.Backend;
using DriveBackend.Models;

namespace DriveBackend
{
    internal class Program
    {
        private enum MovieStore
        {
            GoogleDrive,
            BoringFile
        }

        private const string DriveUrl =
            @"https://spreadsheets.google.com/feeds/list/0Auso2_siq7fodFV0NmNxMVpGdmhWOTBYUDBvSG5GRFE/od6/public/values?alt=json";

        private const string FileName = "movies.json";

        private static void Main(string[] args)
        {
            MovieStore storeChosen = GetMovieStore();
            IBackend movieBackend = GetMovieBackend(storeChosen);

            List<Movie> moviesList = movieBackend.ReadAllMovies();

            PrintMoives(moviesList);
        }

        private static void PrintMoives(List<Movie> moviesList)
        {
            for (int index = 0; index < moviesList.Count; index++)
            {
                Movie movie = moviesList[index];
                Console.WriteLine("Movie No. {0}\n{1}\n", index + 1, movie);
            }
        }

        private static IBackend GetMovieBackend(MovieStore storeChosen)
        {
            IBackend movieBackend = null;
            switch (storeChosen)
            {
                case MovieStore.GoogleDrive:
                    movieBackend = new Backend.DriveBackend(DriveUrl);
                    break;
                case MovieStore.BoringFile:
                    movieBackend = new FileBackend(FileName);
                    break;
            }
            return movieBackend;
        }

        private static MovieStore GetMovieStore()
        {
            Console.Write(
                "Where do you want to get your movies from?\n1. Google Drive\n2. A file from your system\n\n: ");
            while (true)
            {
                string movieStoreOption = Console.ReadLine();
                if ("1".Equals(movieStoreOption))
                {
                    return MovieStore.GoogleDrive;
                }
                else if ("2".Equals(movieStoreOption))
                {
                    return MovieStore.BoringFile;
                }
                else
                {
                    Console.Write("Not a valid option.\n:");
                }
            }
        }
    }
}
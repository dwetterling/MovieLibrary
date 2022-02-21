using System;
using System.Collections.Generic;


namespace MovieLibrary
{
    class Movie
    {
        public string movieID { get; set; }
        public string Title { get; set; }
        public List<string> Genres { get; set; }

        public string DisplayMovie()
        {
            string output = "";
            output = String.Format("Movie ID: {0}\nTitle: {1}\n", movieID, Title);
            string genresList = "";
            for(int i = 0; i< Genres.Count; i++)
            {
                genresList += Genres[i] + ",";
            }
            genresList = genresList.Substring(0, genresList.Length - 1);
            output += "Genres: " + genresList;

            return output;
        }
    }
}
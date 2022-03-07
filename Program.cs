using System;
using System.Linq;
using System.IO;


namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = Directory.GetCurrentDirectory() + "\\nlog.config";

            // create instance of Logger
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            
            logger.Trace("Starting Application");

 
           
            int selection = 0;
            string filePath = "movies.scrubbed.csv";

            MovieFile movieList = new MovieFile(filePath);

            Console.WriteLine("Welcome to the Movie Library!");

            do
            {
                Console.WriteLine("1) View all Movies 2) Add a movie 3) Exit");
                selection = Convert.ToInt32(Console.ReadLine());
                if (selection == 1)
                {
                    foreach (Movie movie in movieList.ReadFile())
                    {
                        Console.WriteLine(movie.DisplayMovie());
                    }
                }
                
                else if (selection == 2)
                {
                    
                    Console.WriteLine("Enter an ID:");
                    string newID = Console.ReadLine();
                    Console.WriteLine("Enter the new title");
                    string newTitle = Console.ReadLine();
                    string genre = "";
                    string temp = "";
                    string director = "";
                    string runningtime = "";
                    do
                    {
                        Console.WriteLine("Enter genres - 'done' when complete");
                        temp = Console.ReadLine();
                        if (temp != "done")
                        {
                            genre += temp + "|";
                        }
                    } while (temp.ToLower() != "done");
                    genre = genre.Substring(0, genre.Length - 1);
                    //ask user for director
                        Console.WriteLine("Enter the director");
                        director = Console.ReadLine();
                        //ask user for movie run time
                        Console.WriteLine("Enter the movies runtime (h:m:s)");
                        runningtime = Console.ReadLine();

                    // Check for duplicates
                    
                    if (movieList.Search(newTitle).Count == 0)
                    {
                        Movie newMovie = new Movie();
                        newMovie.movieID = newID;
                        newMovie.Title = newTitle;
                        newMovie.Genres = genre.Split('|').ToList();
                        newMovie.director = director;
                        newMovie.runningtime = runningtime;
                        movieList.WriteFile(newMovie);
                    }
                    else if (movieList.Search(newTitle).Count == 1){
                        Console.WriteLine("That movie is already in the library");
                    }
                }
            } while (selection != 3);
            
                logger.Trace("Ending Application");
            
        }
    }
}

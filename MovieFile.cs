
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog;

namespace MovieLibrary
{
    class MovieFile
    {
        private Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string filePath { get; set; }

        public MovieFile(string path)
        {
            filePath = path;
        }

        public List<Movie> ReadFile()
        {
            List<Movie> list = new List<Movie>();

            if (File.Exists(filePath))
            {

                StreamReader read = null;
                try
                {
                    read = new StreamReader(filePath);
                    while (!read.EndOfStream)
                    {
                        string id = "";
                        string title = "";
                        string[] genres = null;

                        string line = read.ReadLine();

                        if (line.IndexOf("\"") > -1)
                        {
                            
                            int idx = line.IndexOf(",");
                            id = line.Substring(0, idx);
                            line = line.Substring(idx + 1);
                            idx = line.IndexOf("\"", 1);
                            title = line.Substring(1, idx - 1);
                            string temp = line.Substring(idx + 2);
                            genres = temp.Split('|');

                            var test = temp.Split('|').ToList();
                        }
                        else
                        {
                            string[] values = line.Split(',');
                            id = values[0];
                            title = values[1];
                            genres = values[2].Split('|');
                        }

                        Movie newMovie = new Movie();

                        newMovie.movieID = id;
                        newMovie.Title = title;
                        newMovie.Genres = new List<string>(genres);

                        list.Add(newMovie);
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error reading movie file");
                }
                finally
                {
                    read.Close();
                }
            }
            return list;
        }
        public List<Movie> Search(string searchTerm)
        {
            List<Movie> movies = ReadFile();
            List<Movie> matched = new List<Movie>();

            foreach(Movie record in movies)
            {
                if (record.Title.Contains(searchTerm))
                {
                    matched.Add(record);
                }
            }

            return matched;
        }
        public void WriteFile(Movie newMovie)
        {
            // Write to the file
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(filePath, true);

                sw.WriteLine("{0},{1},{2}", newMovie.movieID, newMovie.Title, String.Join("|", newMovie.Genres));
            }
            catch (Exception e)
            {
                logger.Error(e, "Error writing to data file");
            }
            finally
            {
                sw.Close();
            }
        }
    }
}
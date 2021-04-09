using System;
using System.Collections.Generic;
using System.Linq;
using MVVMTools;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfCinemaApp.JsonClass;
using System.Collections.ObjectModel;
using System.Windows;
using System.Diagnostics;
using System.IO;

namespace WpfCinemaApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Movie> searchMovies;
        public ObservableCollection<Movie> SearchMovies { get => searchMovies; set => OnChanged(out searchMovies, value); }


        private ObservableCollection<Movie> movies;
        public ObservableCollection<Movie> Movies { get => movies; set => OnChanged(out movies, value); }

        private readonly string path = "movies.json";
        public MainViewModel()
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                Movies = JsonSerializer.Deserialize<ObservableCollection<Movie>>(json);
            }
            else  Movies  = new ObservableCollection<Movie>();

            SearchMovies = new ObservableCollection<Movie>();
            Web = new WebClient();
        }

        public void Close()
        {
            try
            {
                var json = JsonSerializer.Serialize(Movies);
                File.WriteAllText(path, json);
            }
            catch (Exception)
            { }
        }
        
        public WebClient Web { get; set; }
        string apiUrlMovie = @"http://www.omdbapi.com/";
        string apiKeyMovie = "5b9b7798";

        public SearchMovies SearchAllTitle()
        {
            try
            {
                var request = $@"{apiUrlMovie}?apikey={apiKeyMovie}&s={MovieName}";
                var answer = Web.DownloadString(request);
                SearchMovies result = System.Text.Json.JsonSerializer.Deserialize<SearchMovies>(answer);
                return result;
            }
            catch (Exception)
            { }
            return null;
        }


        private Command searchCommand;
        public Command SearchCommand => searchCommand ??= new Command(() => {

            SearchMovies.Clear();
            var movies = SearchAllTitle();
            if (movies.Search!=null)
            {
                foreach (var item in movies.Search)
                    SearchMovies.Add(item);
            }
            else
            {
                MessageBox.Show("Movie not found","Error",MessageBoxButton.OK,MessageBoxImage.Error);}     

        },()=> !string.IsNullOrWhiteSpace(MovieName));


        private Command<Movie> addCommand;
        public Command<Movie> AddCommand => addCommand ??= new Command<Movie>((movie) => {


            var request = $"{apiUrlMovie}?apikey={apiKeyMovie}&t={movie.Title}";
            var answer = Web.DownloadString(request);
            var result = JsonSerializer.Deserialize<Movie>(answer);
            Movies.Add(result);
        });

        private Command<Movie> removeCommand;
        public Command<Movie> RemoveCommand => removeCommand ??= new Command<Movie>((movie) => {
            if (movie!=null)
                Movies.Remove(movie);
        });


        private string movieName;
        public string MovieName {
            get => movieName; 
            set {
                OnChanged(out movieName, value);
                SearchCommand.RaiseCanExecuteChanged();
                }
        }

    }
}

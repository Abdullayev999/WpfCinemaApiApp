namespace WpfCinemaApp.JsonClass
{
    public class SearchMovies
    {
        public Movie[] Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }

}

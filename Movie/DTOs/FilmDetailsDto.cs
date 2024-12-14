namespace Movie.DTOs
{
    public class FilmDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public double VoteAverage { get; set; }
        public string ReleaseDate { get; set; }
        public int Runtime { get; set; }
    }
}

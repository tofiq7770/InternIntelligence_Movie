using System.ComponentModel.DataAnnotations;

namespace Movie.DTOs
{
    public class FilmCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        public string Overview { get; set; }
    }
}

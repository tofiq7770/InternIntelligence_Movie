using System.ComponentModel.DataAnnotations;

namespace Movie.Models
{

    public class Film : BaseEntity
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
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }

}

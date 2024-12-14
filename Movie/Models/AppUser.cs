using Microsoft.AspNetCore.Identity;

namespace Movie.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Film> Films { get; set; }

    }
}

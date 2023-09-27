using Microsoft.AspNetCore.Identity;

namespace USTM_Library.Models
{
    public class AppUser : IdentityUser
    {
        public int IdUser { get; set; }
    }
}

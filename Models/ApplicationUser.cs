using Microsoft.AspNetCore.Identity;
namespace Web1670.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Firstname { get; set;}
        public string Lastname { get; set;}
    }
}

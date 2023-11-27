using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FineArt.Models
{
	public class ApplicationUser : IdentityUser
	{
        [Required]
        public string First_name { get; set; }
        [Required]
        public string Last_name { get; set; }
    }
}

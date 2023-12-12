using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineArt.Models
{
	public class AdminManager
	{
		[Key]
        public int AdminManagerId { get; set; }
		public int? Age { get; set; }
		public string? Gender { get; set; }
		public string? Phone { get; set; }
		public int? WalletAmount { get; set; }
        public string? Extras { get; set; }
        public int? ApprovedStatus { get; set; }

        [DisplayName("Upload Avatar")]
		public string? AvatarUrl { get; set; }

		[NotMapped]
		[DisplayName("Upload Avatar")]
		public IFormFile? AvatarFile { get; set; }

		public string UserId { get; set; }
		public IdentityUser User { get; set; }

        public ICollection<Exhibition> exhibitions { get; set; }
        public ICollection<Notification> notifications { get; set; }
    }
}

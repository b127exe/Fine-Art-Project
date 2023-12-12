using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineArt.Models
{
	public class Student
	{
		[Key]
        public int StudentId { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? ArtType { get; set; }
        public int? WalletAmount { get; set; }
        public string? Standard { get; set; }

        [DisplayName("Upload Avatar")]
        public string? AvatarUrl { get; set; }

        [NotMapped]
		[DisplayName("Upload Avatar")]
		public IFormFile? AvatarFile { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }


        public ICollection<Posting> postings { get; set; }
        public ICollection<PostingSubmission> postingSubmissions { get; set; }
        public ICollection<AwardedStudent> awardedStudents { get; set; }
        public ICollection<Notification> notifications { get; set; }
    }
}

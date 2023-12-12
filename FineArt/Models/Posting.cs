using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineArt.Models
{
	public class Posting
	{
		[Key]
		public int PostingId { get; set; }
		[Required]
        public string Title { get; set; }

		[MaxLength(500)]
		public string Description { get; set; }
		[Required]
        public string PostedDate { get; set; }
		[Required]
        public string Quotation { get; set; }

		[DefaultValue(0)]
		public int PaidStatus { get; set; }

        [DisplayName("Upload Design")]
        public string? DesignImageUrl { get; set; }

		[NotMapped]
		[DisplayName("Upload Design")]
		public IFormFile? DesignImageFile { get; set; }

		[ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public ICollection<PostingSubmission> postingSubmissions { get; set; }
        public ICollection<ExhibitionSubmission> exhibitionSubmissions { get; set; }
        public ICollection<AwardedStudent> awardedStudents { get; set; }
        public ICollection<Notification> notifications { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineArt.Models
{
	public class CustomerExhibitionPost
	{
		[Key]
        public int CustomerExPostId { get; set; }
		[Required]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string CardNumber { get; set; }
		[Required]
		public string Cvc { get; set; }

		[ForeignKey("ExhibitionSubmission")]
        public int ExSubmissionId { get; set; }
        public ExhibitionSubmission ExhibitionSubmission { get; set; }
    }
}

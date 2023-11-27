using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineArt.Models
{
	public class SubmissionRemarks
	{
		[Key]
        public int SRemarksId { get; set; }
		[Required]
		public MarkStatus Marks { get; set; }
		[MaxLength(500)]
        public string Comments { get; set; }

		[ForeignKey("PostingSubmission")]
        public int SubmissionId { get; set; }
        public PostingSubmission PostingSubmission { get; set; }

    }
	public enum MarkStatus
	{
		Best,
		Better,
		Good,
		Moderate,
		Normal,
		Disqualified
	}
}

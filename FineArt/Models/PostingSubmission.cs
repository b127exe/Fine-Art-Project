using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineArt.Models
{
	public class PostingSubmission
	{
		[Key]
        public int SubmissionId { get; set; }
		[Required]
        public string SubmissionDate { get; set; }
		[Required]
		public string SubmissionQuote { get; set; }

		[ForeignKey("Competition")]
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
        public int? SubmissionStatus { get; set; }

        [ForeignKey("Posting")]
        public int PostingId { get; set; }
        public Posting Posting { get; set; }

		[ForeignKey("Student")]
		public int StudentId { get; set; }
		public Student Student { get; set; }

        public ICollection<SubmissionRemarks> submissionRemarks { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineArt.Models
{
	public class Competition
	{
		[Key]
        public int CompetitionId { get; set; }
        [Required]
        public string CompetitionTitle { get; set; }
        public int Status { get; set; }

        [Required]
		public string StartDate { get; set; }
		[Required]
		public string EndDate { get; set; }
		[Required]
		public string Conditions { get; set; }

		[ForeignKey("Award")]
        public int AwardId { get; set; }
        public Award Award { get; set; }

        public ICollection<PostingSubmission> postingSubmissions { get; set; }
        public ICollection<AwardedStudent> awardedStudents { get; set; }
        public ICollection<Notification> notifications { get; set; }
    }
}

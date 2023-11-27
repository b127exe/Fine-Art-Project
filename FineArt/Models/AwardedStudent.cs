using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineArt.Models
{
	public class AwardedStudent
	{
		[Key]
        public int AwardedStudentId { get; set; }
        public string? AwardRemarks { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Posting")]
        public int PostingId { get; set; }
        public Posting Posting { get; set; }

        [ForeignKey("Competition")]
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
    }
}

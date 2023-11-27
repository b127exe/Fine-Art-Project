using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineArt.Models
{
	public class ExhibitionSubmission
	{
		[Key]
        public int ExSubmissionId { get; set; }
		[Required]
        public int Price { get; set; }
        public int? SoldStatus { get; set; }

        [ForeignKey("Posting")]
        public int PostingId { get; set; }
        public Posting Posting { get; set; }

        [ForeignKey("Exhibition")]
        public int ExhibitionId { get; set; }
        public Exhibition Exhibition { get; set; }

        public ICollection<CustomerExhibitionPost> customerExhibitionPosts { get; set; }
    }
}

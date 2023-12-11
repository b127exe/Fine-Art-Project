using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineArt.Models
{
	public class Exhibition
	{
		[Key]
		public int ExhibitionId { get; set; }
        [Required]
        public string ExhibitionTitle { get; set; }
        [Required]
        public string ExhibitionDate { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string Conditions { get; set; }
        public int ExStatus { get; set; }

        [ForeignKey("AdminManager")]
        public int AdminManagerId { get; set; }
        public AdminManager AdminManager { get; set; }

        public string Country { get; set; }

        public ICollection<ExhibitionSubmission> exhibitionSubmissions { get; set; }
    }
}

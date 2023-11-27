using System.ComponentModel.DataAnnotations;

namespace FineArt.Models
{
	public class Award
	{
		[Key]
        public int AwardId { get; set; }
        [Required]
        public string AwardTitle { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int AwardAmount { get; set; }
        public string? Terms { get; set; }

        public ICollection<Competition> competitions { get; set; }
    }
}

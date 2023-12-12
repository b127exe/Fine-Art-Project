using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineArt.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public string NotType { get; set; }
        public DateTime NotDate { get; set; }

        [DefaultValue(0)]
        public int? NotShowHide { get; set; }

        [ForeignKey("Posting")]
        [DefaultValue(null)]
        public int? PostingId { get; set; }
        public Posting Posting { get; set; }

        [ForeignKey("Student")]
        [DefaultValue(null)]
        public int? StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("AdminManager")]
        [DefaultValue(null)]
        public int? AdminManagerId { get; set; }
        public AdminManager AdminManager { get; set; }

        [ForeignKey("Teacher")]
        [DefaultValue(null)]
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

    }
}

namespace FineArt.Models
{
    public class FineArtViewModel
    {
        public List<Competition> myCompetitions { get; set; }
        public List<Posting> myPostings { get; set; }
        public List<PostingSubmission> myPostingSubmissions { get; set; }
        public List<AwardedStudent> myAwardedStudents { get; set; }
        public Posting inputPosting { get; set; }
        public PostingSubmission inputPostingSubmission { get; set; }
    }
}

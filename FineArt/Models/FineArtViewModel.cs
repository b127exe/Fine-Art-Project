namespace FineArt.Models
{
    public class FineArtViewModel
    {
        public List<Competition> myCompetitions { get; set; }
        public List<Competition> ongoingCompetitions { get; set; }
        public List<Posting> myPostings { get; set; }
        public List<PostingSubmission> myPostingSubmissions { get; set; }
        public List<AwardedStudent> myAwardedStudents { get; set; }
        public Posting inputPosting { get; set; }
        public PostingSubmission inputPostingSubmission { get; set; }
        public Competition inputCompetition { get; set; }
        public SubmissionRemarks inputSubmissionRemarks { get; set; }
        public List<SubmissionRemarks> mySubmissionRemarks { get; set; }
        public List<Exhibition> myExhibitions { get; set; }
    }
}

using FineArt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FineArt.Data
{
	public class FineArtDbContext : IdentityDbContext
	{
        public FineArtDbContext(DbContextOptions<FineArtDbContext> options) : base(options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<AdminManager> AdminManager { get; set; }
        public DbSet<Posting> Postings { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<AwardedStudent> AwardedStudents { get; set; }
        public DbSet<PostingSubmission> PostingSubmissions { get; set; }
        public DbSet<SubmissionRemarks> SubmissionRemarks { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<ExhibitionSubmission> ExhibitionSubmissions { get; set; }
        public DbSet<CustomerExhibitionPost> CustomerExhibitionPosts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .IsRequired();

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .IsRequired();

            modelBuilder.Entity<AdminManager>()
                .HasOne(am => am.User)
                .WithMany()
                .HasForeignKey(am => am.UserId)
                .IsRequired();

            modelBuilder.Entity<ExhibitionSubmission>()
                .HasOne(es => es.Posting)
                .WithMany(p => p.exhibitionSubmissions)
                .HasForeignKey(es => es.PostingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AwardedStudent>()
                .HasOne(aw => aw.Student)
                .WithMany(s => s.awardedStudents)
                .HasForeignKey(aw => aw.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PostingSubmission>()
                .HasOne(ps => ps.Student)
                .WithMany(s => s.postingSubmissions)
                .HasForeignKey(ps => ps.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MultiLanguageExamManagementSystem.Models.Entities;

namespace MultiLanguageExamManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LocalizationResource> LocalizationResources { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Exam_Question> Exam_Questions { get; set; }
        public DbSet<TakenExam> TakenExams { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //One contry can have multiple languages, once you delete a country, all languages related to it should be deleted, but not vice versa
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Languages)
                .WithOne(l => l.Country)
                .HasForeignKey(l => l.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            // One language can have multiple localization resources, once you delete a language, all localization resources related to it should be deleted, but not vice versa
            modelBuilder.Entity<Language>()
                .HasMany(l => l.LocalizationResources)
                .WithOne(lr => lr.Language)
                .HasForeignKey(lr => lr.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);

            //there can be two records that have the same namespace and key only if they have different LanguageId
            modelBuilder.Entity<LocalizationResource>()
                .HasIndex(lr => new { lr.Namespace, lr.Key, lr.LanguageId })
                .IsUnique();




            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Professor)
                .WithMany()
                .HasForeignKey(e => e.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exam_Question>()
                 .HasKey(eq => new { eq.ExamId, eq.QuestionId });

            modelBuilder.Entity<Exam_Question>()
                .HasOne(eq => eq.Exam)
                .WithMany(e => e.Exam_Questions)
                .HasForeignKey(eq => eq.ExamId);

            modelBuilder.Entity<Exam_Question>()
                .HasOne(eq => eq.Question)
                .WithMany(q => q.Exam_Questions)
                .HasForeignKey(eq => eq.QuestionId);

            modelBuilder.Entity<TakenExam>()
                .HasOne(te => te.User)
                .WithMany()
                .HasForeignKey(te => te.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TakenExam>()
                .HasOne(te => te.Exam)
                .WithMany()
                .HasForeignKey(te => te.ExamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

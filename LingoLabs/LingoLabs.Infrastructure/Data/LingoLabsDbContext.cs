using LingoLabs.Domain.Entities;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Domain.Entities.Languages;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Data
{
    public class LingoLabsDbContext : DbContext
    {
        public DbSet<LearningStyle> LearningStyles { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<LanguageLevelResult> LanguageLevelResults { get; set; }
        public DbSet<ChapterResult> ChapterResults { get; set; }
        public DbSet<LanguageCompetenceResult> LanguageCompetenceResults { get; set; }
        public DbSet<LessonResult> LessonResults { get; set; }
        public DbSet<QuestionResult> QuestionResults { get; set; }
        public DbSet<ReadingQuestionResult> ReadingQuestionResults { get; set; }
        public DbSet<WritingQuestionResult> WritingQuestionResults { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageLevel> LanguageLevels { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<LanguageCompetence> LanguageCompetences { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<ListeningLesson> ListeningLessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<EntityTag> EntityTags { get; set; }
        public DbSet<UserLanguageLevel> UserLanguageLevels { get; set; }

        public LingoLabsDbContext(DbContextOptions<LingoLabsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ENROLLMENT

            modelBuilder.Entity<Enrollment>()
                .HasMany(e => e.LanguageLevelResults)
                .WithOne()
                .HasForeignKey("EnrollmentId");

            modelBuilder.Entity<Enrollment>()
                .HasMany(e => e.UserLanguageLevels) 
                .WithOne()
                .HasForeignKey(u => u.EnrollmentId);

            modelBuilder.Entity<LanguageLevelResult>()
                .HasOne(l => l.Enrollment)
                .WithMany(e => e.LanguageLevelResults)
                .HasForeignKey(l => l.EnrollmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LanguageLevelResult>()
                .HasMany(l => l.ChapterResults)
                .WithOne()
                .HasForeignKey("LanguageLevelResultId");

            modelBuilder.Entity<LanguageLevelResult>()
                .HasOne(l => l.LanguageLevel) 
                .WithMany()
                .HasForeignKey(l => l.LanguageLevelId);

            modelBuilder.Entity<ChapterResult>()
                .HasOne(c => c.LanguageLevelResult)
                .WithMany(l => l.ChapterResults)
                .HasForeignKey(c => c.LanguageLevelResultId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChapterResult>()
                .HasOne(c => c.Chapter)
                .WithMany()
                .HasForeignKey(c => c.ChapterId);

            modelBuilder.Entity<ChapterResult>()
                .HasMany(c => c.LessonResults)
                .WithOne()
                .HasForeignKey("ChapterResultId");

            //modelBuilder.Entity<LanguageCompetenceResult>()
            //    .HasOne(l => l.ChapterResult)
            //    .WithMany(c => c)
            //    .HasForeignKey(l => l.ChapterResultId)
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LanguageCompetenceResult>()
                .HasOne(l => l.Enrollment)
                .WithMany(e => e.LanguageCompetenceResults)
                .HasForeignKey(l => l.EnrollmentId);


            modelBuilder.Entity<LanguageCompetenceResult>()
                .HasOne(l => l.LanguageCompetence)
                .WithMany()
                .HasForeignKey(l => l.LanguageCompetenceId);

            modelBuilder.Entity<LessonResult>()
                .HasOne(l => l.LanguageCompetenceResult)
                .WithMany(l => l.LessonsResults)
                .HasForeignKey(l => l.LanguageCompetenceResultId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<LessonResult>()
                .HasOne(l => l.ChapterResult)
                .WithMany(l => l.LessonResults)
                .HasForeignKey(l => l.ChapterResultId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LessonResult>()
                .HasOne(l => l.Lesson)
                .WithMany()
                .HasForeignKey(l => l.LessonId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LessonResult>()
                .HasMany(l => l.QuestionResults)
                .WithOne()
                .HasForeignKey("LessonResultId");

            modelBuilder.Entity<QuestionResult>()
                .HasOne(q => q.LessonResult)
                .WithMany(l => l.QuestionResults)
                .HasForeignKey(q => q.LessonResultId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<QuestionResult>()
                .HasOne(q => q.Question)
                .WithMany()
                .HasForeignKey(q => q.QuestionId);

            //modelBuilder.Entity<UserLanguageLevel>()
            //    .HasKey(u => new { u.EnrollmentId, u.LanguageCompetenceId, u.LanguageLevelId });

            modelBuilder.Entity<UserLanguageLevel>()
                .HasOne(u => u.Enrollment)
                .WithMany(e => e.UserLanguageLevels)
                .HasForeignKey(u => u.EnrollmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserLanguageLevel>()
                .HasOne(u => u.LanguageCompetence)
                .WithMany(e => e.UserLanguageLevels)
                .HasForeignKey(u => u.LanguageCompetenceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserLanguageLevel>()
                .HasOne(u => u.LanguageLevel)
                .WithMany(e => e.UserLanguageLevels)
                .HasForeignKey(u => u.LanguageLevelId);

            // LANGUAGE

            modelBuilder.Entity<Language>()
                .HasMany(l => l.LanguageLevels)
                .WithOne()
                .HasForeignKey("LanguageId");

            modelBuilder.Entity<Language>()
                .HasMany(l => l.LanguageCompetences)
                .WithOne()
                .HasForeignKey("LanguageId");

            modelBuilder.Entity<Language>()
                .HasMany(l => l.PlacementTest)
                .WithOne()
                .HasForeignKey("LanguageId");

            modelBuilder.Entity<Language>()
                .HasMany(l => l.LanguageTags)
                .WithOne(et => et.Language)
                .HasForeignKey(et => et.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LanguageLevel>()
                .HasOne(l => l.Language)
                .WithMany(l => l.LanguageLevels)
                .HasForeignKey(l => l.LanguageId);

            modelBuilder.Entity<LanguageLevel>()
                .HasMany(l => l.LanguageLevelChapters)
                .WithOne()
                .HasForeignKey("LanguageLevelId");

            modelBuilder.Entity<LanguageLevel>()
                .HasMany(l => l.LanguageLevelTags)
                .WithOne(et => et.LanguageLevel)
                .HasForeignKey(et => et.LanguageLevelId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Chapter>()
                .HasOne(c => c.LanguageLevel)
                .WithMany(l => l.LanguageLevelChapters)
                .HasForeignKey(c => c.LanguageLevelId);

            modelBuilder.Entity<Chapter>()
                .HasMany(l => l.ChapterTags)
                .WithOne(et => et.Chapter)
                .HasForeignKey(et => et.ChapterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LanguageCompetence>()
                .HasOne(l => l.Language)
                .WithMany(l => l.LanguageCompetences)
                .HasForeignKey(l => l.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LanguageCompetence>()
                .HasMany(l => l.LanguageCompetenceLessons)
                .WithOne(c => c.LanguageCompetence)
                .HasForeignKey(c => c.LanguageCompetenceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LanguageCompetence>()
                .HasMany(lc => lc.UserLanguageLevels)
                .WithOne(ull => ull.LanguageCompetence)
                .HasForeignKey(ull => ull.LanguageCompetenceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LanguageCompetence>()
                .HasOne(l => l.Language)
                .WithMany(c => c.LanguageCompetences)
                .HasForeignKey(l => l.LanguageId);

            modelBuilder.Entity<LanguageCompetence>()
                .HasMany(l => l.LearningCompetenceTags)
                .WithOne(et => et.LanguageCompetence)
                .HasForeignKey(et => et.LanguageCompetenceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Chapter)
                .WithMany(l => l.ChapterLessons)
                .HasForeignKey(l => l.ChapterId);

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.LessonQuestions)
                .WithOne()
                .HasForeignKey("LessonId");

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.LessonTags)
                .WithOne(et => et.Lesson)
                .HasForeignKey(et => et.LessonId);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Lesson)
                .WithMany(l => l.LessonQuestions)
                .HasForeignKey(q => q.LessonId);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.QuestionChoices)
                .WithOne()
                .HasForeignKey("QuestionId");

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Language)
                .WithMany(l => l.PlacementTest)
                .HasForeignKey(q => q.LanguageId);

            modelBuilder.Entity<Choice>()
                .HasOne(c => c.Question)
                .WithMany(q => q.QuestionChoices)
                .HasForeignKey(c => c.QuestionId);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.EntityTags)
                .WithOne(et => et.Tag)
                .HasForeignKey(et => et.TagId);
        }
    }
}

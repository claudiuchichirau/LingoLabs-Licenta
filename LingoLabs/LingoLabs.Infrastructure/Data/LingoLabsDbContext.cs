﻿using LingoLabs.Domain.Entities;
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
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageLevel> LanguageLevels { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<LanguageCompetence> LanguageCompetences { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LingoLabsDB;Trusted_Connection=True;");
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
                .HasForeignKey(c => c.LanguageLevelResultId);

            modelBuilder.Entity<ChapterResult>()
                .HasOne(c => c.Chapter)
                .WithMany()
                .HasForeignKey(c => c.ChapterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChapterResult>()
                .HasMany(c => c.LanguageCompetenceResults)
                .WithOne()
                .HasForeignKey("ChapterResultId");

            modelBuilder.Entity<LanguageCompetenceResult>()
                .HasOne(l => l.ChapterResult)
                .WithMany(c => c.LanguageCompetenceResults)
                .HasForeignKey(l => l.ChapterResultId);

            modelBuilder.Entity<LanguageCompetenceResult>()
                .HasOne(l => l.LanguageCompetence)
                .WithMany()
                .HasForeignKey(l => l.LanguageCompetenceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LanguageCompetenceResult>()
                .HasMany(l => l.LessonsResults)
                .WithOne()
                .HasForeignKey("LanguageCompetenceResultId");

            modelBuilder.Entity<LessonResult>()
                .HasOne(l => l.LanguageCompetenceResult)
                .WithMany(l => l.LessonsResults)
                .HasForeignKey(l => l.LanguageCompetenceResultId);

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
                .HasForeignKey(q => q.LessonResultId);

            modelBuilder.Entity<QuestionResult>()
                .HasOne(q => q.Question)
                .WithMany()
                .HasForeignKey(q => q.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserLanguageLevel>()
                .HasKey(u => new { u.EnrollmentId, u.LanguageCompetenceId, u.LanguageLevelId });

            modelBuilder.Entity<UserLanguageLevel>()
                .HasOne(u => u.Enrollment)
                .WithMany(e => e.UserLanguageLevels)
                .HasForeignKey(u => u.EnrollmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserLanguageLevel>()
                .HasOne(u => u.LanguageCompetence)
                .WithMany()
                .HasForeignKey(u => u.LanguageCompetenceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserLanguageLevel>()
                .HasOne(u => u.LanguageLevel)
                .WithMany()
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
                .HasMany(l => l.LanguageKeyWords)
                .WithOne()
                .HasForeignKey("LanguageId");

            modelBuilder.Entity<LanguageLevel>()
                .HasOne(l => l.Language)
                .WithMany(l => l.LanguageLevels)
                .HasForeignKey(l => l.LanguageId);

            modelBuilder.Entity<LanguageLevel>()
                .HasMany(l => l.LanguageChapters)
                .WithOne()
                .HasForeignKey("LanguageLevelId");

            modelBuilder.Entity<LanguageLevel>()
                .HasMany(l => l.LanguageLeveKeyWords)
                .WithOne()
                .HasForeignKey("LanguageLevelId");

            modelBuilder.Entity<Chapter>()
                .HasOne(c => c.LanguageLevel)
                .WithMany(l => l.LanguageChapters)
                .HasForeignKey(c => c.LanguageLevelId);

            modelBuilder.Entity<Chapter>()
                .HasMany(c => c.languageCompetences)
                .WithOne()
                .HasForeignKey("ChapterId");

            modelBuilder.Entity<Chapter>()
                .HasMany(c => c.ChapterKeyWords)
                .WithOne()
                .HasForeignKey("ChapterId");

            modelBuilder.Entity<LanguageCompetence>()
                .HasOne(l => l.Language)
                .WithMany(l => l.LanguageCompetences)
                .HasForeignKey(l => l.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LanguageCompetence>()
                .HasOne(l => l.Chapter)
                .WithMany(c => c.languageCompetences)
                .HasForeignKey(l => l.ChapterId);

            modelBuilder.Entity<LanguageCompetence>()
                .HasMany(l => l.Lessons)
                .WithOne()
                .HasForeignKey("LanguageCompetenceId");

            modelBuilder.Entity<LanguageCompetence>()
                .HasMany(l => l.LearningCompetenceKeyWords)
                .WithOne()
                .HasForeignKey("LanguageCompetenceId");

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.LanguageCompetence)
                .WithMany(l => l.Lessons)
                .HasForeignKey(l => l.LanguageCompetenceId);

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.LessonQuestions)
                .WithOne()
                .HasForeignKey("LessonId");

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Lesson)
                .WithMany(l => l.LessonQuestions)
                .HasForeignKey(q => q.LessonId);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.QuestionChoices)
                .WithOne()
                .HasForeignKey("QuestionId");

            modelBuilder.Entity<Choice>()
                .HasOne(c => c.Question)
                .WithMany(q => q.QuestionChoices)
                .HasForeignKey(c => c.QuestionId);
        }
    }
}

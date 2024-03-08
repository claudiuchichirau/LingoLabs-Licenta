﻿// <auto-generated />
using System;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LingoLabs.Infrastructure.Migrations
{
    [DbContext(typeof(LingoLabsDbContext))]
    partial class LingoLabsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.ChapterResult", b =>
                {
                    b.Property<Guid>("ChapterResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChapterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("LanguageLevelResultId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ChapterResultId");

                    b.HasIndex("ChapterId");

                    b.HasIndex("LanguageLevelResultId");

                    b.ToTable("ChapterResults");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.Enrollment", b =>
                {
                    b.Property<Guid>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("LanguageId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.LanguageCompetenceResult", b =>
                {
                    b.Property<Guid>("LanguageCompetenceResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChapterResultId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("LanguageCompetenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LanguageCompetenceResultId");

                    b.HasIndex("ChapterResultId");

                    b.HasIndex("LanguageCompetenceId");

                    b.ToTable("LanguageCompetenceResults");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.LanguageLevelResult", b =>
                {
                    b.Property<Guid>("LanguageLevelResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EnrollmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("LanguageLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LanguageLevelResultId");

                    b.HasIndex("EnrollmentId");

                    b.HasIndex("LanguageLevelId");

                    b.ToTable("LanguageLevelResults");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.LessonResult", b =>
                {
                    b.Property<Guid>("LessonResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("LanguageCompetenceResultId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LessonResultId");

                    b.HasIndex("LanguageCompetenceResultId");

                    b.HasIndex("LessonId");

                    b.ToTable("LessonResults");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.QuestionResult", b =>
                {
                    b.Property<Guid>("QuestionResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LessonResultId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("QuestionResultId");

                    b.HasIndex("LessonResultId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionResults");

                    b.HasDiscriminator<string>("Discriminator").HasValue("QuestionResult");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.UserLanguageLevel", b =>
                {
                    b.Property<Guid>("EnrollmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LanguageCompetenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LanguageLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserLanguageLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EnrollmentId", "LanguageCompetenceId", "LanguageLevelId");

                    b.HasIndex("LanguageCompetenceId");

                    b.HasIndex("LanguageLevelId");

                    b.ToTable("UserLanguageLevels");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Chapter", b =>
                {
                    b.Property<Guid>("ChapterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChapterDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ChapterImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ChapterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ChapterNumber")
                        .HasColumnType("int");

                    b.Property<string>("ChapterVideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LanguageLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ChapterId");

                    b.HasIndex("LanguageLevelId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Choice", b =>
                {
                    b.Property<Guid>("ChoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChoiceContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ChoiceId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Choices");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Language", b =>
                {
                    b.Property<Guid>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LanguageDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageVideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.LanguageCompetence", b =>
                {
                    b.Property<Guid>("LanguageCompetenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChapterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LanguageCompetenceDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageCompetenceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LanguageCompetenceType")
                        .HasColumnType("int");

                    b.Property<string>("LanguageCompetenceVideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LanguageCompetenceId");

                    b.HasIndex("ChapterId");

                    b.HasIndex("LanguageId");

                    b.ToTable("LanguageCompetences");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.LanguageLevel", b =>
                {
                    b.Property<Guid>("LanguageLevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LanguageLevelAlias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageLevelDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageLevelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageLevelVideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LanguageLevelId");

                    b.HasIndex("LanguageId");

                    b.ToTable("LanguageLevels");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Lesson", b =>
                {
                    b.Property<Guid>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<Guid>("LanguageCompetenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LessonContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LessonDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("LessonImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("LessonRequirement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LessonTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LessonType")
                        .HasColumnType("int");

                    b.Property<string>("LessonVideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LessonId");

                    b.HasIndex("LanguageCompetenceId");

                    b.ToTable("Lessons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Lesson");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Question", b =>
                {
                    b.Property<Guid>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("QuestionImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("QuestionLearningType")
                        .HasColumnType("int");

                    b.Property<string>("QuestionRequirement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionVideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("LessonId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Tag", b =>
                {
                    b.Property<Guid>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChapterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LanguageCompetenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LanguageLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LearningStyleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TagContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.HasIndex("ChapterId");

                    b.HasIndex("LanguageCompetenceId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("LanguageLevelId");

                    b.HasIndex("LearningStyleId");

                    b.HasIndex("LessonId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.LearningStyle", b =>
                {
                    b.Property<Guid>("LearningStyleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LearningStyleDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LearningStyleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LearningType")
                        .HasColumnType("int");

                    b.HasKey("LearningStyleId");

                    b.ToTable("LearningStyles");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.ReadingQuestionResult", b =>
                {
                    b.HasBaseType("LingoLabs.Domain.Entities.Enrollments.QuestionResult");

                    b.Property<byte[]>("AudioData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RecognizedText")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ReadingQuestionResult");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.WritingQuestionResult", b =>
                {
                    b.HasBaseType("LingoLabs.Domain.Entities.Enrollments.QuestionResult");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RecognizedText")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("QuestionResults", t =>
                        {
                            t.Property("RecognizedText")
                                .HasColumnName("WritingQuestionResult_RecognizedText");
                        });

                    b.HasDiscriminator().HasValue("WritingQuestionResult");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.ListeningLesson", b =>
                {
                    b.HasBaseType("LingoLabs.Domain.Entities.Languages.Lesson");

                    b.Property<string>("Accents")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AudioContents")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ListeningLesson");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.ChapterResult", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Languages.Chapter", "Chapter")
                        .WithMany()
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LingoLabs.Domain.Entities.Enrollments.LanguageLevelResult", "LanguageLevelResult")
                        .WithMany("ChapterResults")
                        .HasForeignKey("LanguageLevelResultId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Chapter");

                    b.Navigation("LanguageLevelResult");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.Enrollment", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Languages.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.LanguageCompetenceResult", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Enrollments.ChapterResult", "ChapterResult")
                        .WithMany("LanguageCompetenceResults")
                        .HasForeignKey("ChapterResultId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LingoLabs.Domain.Entities.Languages.LanguageCompetence", "LanguageCompetence")
                        .WithMany()
                        .HasForeignKey("LanguageCompetenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChapterResult");

                    b.Navigation("LanguageCompetence");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.LanguageLevelResult", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Enrollments.Enrollment", "Enrollment")
                        .WithMany("LanguageLevelResults")
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LingoLabs.Domain.Entities.Languages.LanguageLevel", "LanguageLevel")
                        .WithMany()
                        .HasForeignKey("LanguageLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enrollment");

                    b.Navigation("LanguageLevel");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.LessonResult", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Enrollments.LanguageCompetenceResult", "LanguageCompetenceResult")
                        .WithMany("LessonsResults")
                        .HasForeignKey("LanguageCompetenceResultId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LingoLabs.Domain.Entities.Languages.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LanguageCompetenceResult");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.QuestionResult", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Enrollments.LessonResult", "LessonResult")
                        .WithMany("QuestionResults")
                        .HasForeignKey("LessonResultId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LingoLabs.Domain.Entities.Languages.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LessonResult");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.UserLanguageLevel", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Enrollments.Enrollment", "Enrollment")
                        .WithMany("UserLanguageLevels")
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LingoLabs.Domain.Entities.Languages.LanguageCompetence", "LanguageCompetence")
                        .WithMany("UserLanguageLevels")
                        .HasForeignKey("LanguageCompetenceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LingoLabs.Domain.Entities.Languages.LanguageLevel", "LanguageLevel")
                        .WithMany()
                        .HasForeignKey("LanguageLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enrollment");

                    b.Navigation("LanguageCompetence");

                    b.Navigation("LanguageLevel");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Chapter", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Languages.LanguageLevel", "LanguageLevel")
                        .WithMany("LanguageChapters")
                        .HasForeignKey("LanguageLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LanguageLevel");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Choice", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Languages.Question", "Question")
                        .WithMany("QuestionChoices")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.LanguageCompetence", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Languages.Chapter", "Chapter")
                        .WithMany("languageCompetences")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LingoLabs.Domain.Entities.Languages.Language", "Language")
                        .WithMany("LanguageCompetences")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Chapter");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.LanguageLevel", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Languages.Language", "Language")
                        .WithMany("LanguageLevels")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Lesson", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Languages.LanguageCompetence", "LanguageCompetence")
                        .WithMany("Lessons")
                        .HasForeignKey("LanguageCompetenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LanguageCompetence");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Question", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Languages.Language", null)
                        .WithMany("PlacementTest")
                        .HasForeignKey("LanguageId");

                    b.HasOne("LingoLabs.Domain.Entities.Languages.Lesson", "Lesson")
                        .WithMany("LessonQuestions")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Tag", b =>
                {
                    b.HasOne("LingoLabs.Domain.Entities.Languages.Chapter", null)
                        .WithMany("ChapterKeyWords")
                        .HasForeignKey("ChapterId");

                    b.HasOne("LingoLabs.Domain.Entities.Languages.LanguageCompetence", null)
                        .WithMany("LearningCompetenceKeyWords")
                        .HasForeignKey("LanguageCompetenceId");

                    b.HasOne("LingoLabs.Domain.Entities.Languages.Language", null)
                        .WithMany("LanguageKeyWords")
                        .HasForeignKey("LanguageId");

                    b.HasOne("LingoLabs.Domain.Entities.Languages.LanguageLevel", null)
                        .WithMany("LanguageLeveKeyWords")
                        .HasForeignKey("LanguageLevelId");

                    b.HasOne("LingoLabs.Domain.Entities.LearningStyle", null)
                        .WithMany("LearningStyleKeyWords")
                        .HasForeignKey("LearningStyleId");

                    b.HasOne("LingoLabs.Domain.Entities.Languages.Lesson", null)
                        .WithMany("LessonTags")
                        .HasForeignKey("LessonId");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.ChapterResult", b =>
                {
                    b.Navigation("LanguageCompetenceResults");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.Enrollment", b =>
                {
                    b.Navigation("LanguageLevelResults");

                    b.Navigation("UserLanguageLevels");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.LanguageCompetenceResult", b =>
                {
                    b.Navigation("LessonsResults");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.LanguageLevelResult", b =>
                {
                    b.Navigation("ChapterResults");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Enrollments.LessonResult", b =>
                {
                    b.Navigation("QuestionResults");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Chapter", b =>
                {
                    b.Navigation("ChapterKeyWords");

                    b.Navigation("languageCompetences");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Language", b =>
                {
                    b.Navigation("LanguageCompetences");

                    b.Navigation("LanguageKeyWords");

                    b.Navigation("LanguageLevels");

                    b.Navigation("PlacementTest");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.LanguageCompetence", b =>
                {
                    b.Navigation("LearningCompetenceKeyWords");

                    b.Navigation("Lessons");

                    b.Navigation("UserLanguageLevels");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.LanguageLevel", b =>
                {
                    b.Navigation("LanguageChapters");

                    b.Navigation("LanguageLeveKeyWords");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Lesson", b =>
                {
                    b.Navigation("LessonQuestions");

                    b.Navigation("LessonTags");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.Languages.Question", b =>
                {
                    b.Navigation("QuestionChoices");
                });

            modelBuilder.Entity("LingoLabs.Domain.Entities.LearningStyle", b =>
                {
                    b.Navigation("LearningStyleKeyWords");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLabs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageVideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageFlag = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "LearningStyles",
                columns: table => new
                {
                    LearningStyleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LearningStyleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LearningStyleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LearningType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningStyles", x => x.LearningStyleId);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_Enrollments_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageLevels",
                columns: table => new
                {
                    LanguageLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageLevelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageLevelAlias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageLevelDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageLevelVideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityNumber = table.Column<int>(type: "int", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageLevels", x => x.LanguageLevelId);
                    table.ForeignKey(
                        name: "FK_LanguageLevels_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LearningStyleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tags_LearningStyles_LearningStyleId",
                        column: x => x.LearningStyleId,
                        principalTable: "LearningStyles",
                        principalColumn: "LearningStyleId");
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChapterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChapterDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChapterPriorityNumber = table.Column<int>(type: "int", nullable: true),
                    ChapterImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ChapterVideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.ChapterId);
                    table.ForeignKey(
                        name: "FK_Chapters_LanguageLevels_LanguageLevelId",
                        column: x => x.LanguageLevelId,
                        principalTable: "LanguageLevels",
                        principalColumn: "LanguageLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageLevelResults",
                columns: table => new
                {
                    LanguageLevelResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    LanguageLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageLevelResults", x => x.LanguageLevelResultId);
                    table.ForeignKey(
                        name: "FK_LanguageLevelResults_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentId");
                    table.ForeignKey(
                        name: "FK_LanguageLevelResults_LanguageLevels_LanguageLevelId",
                        column: x => x.LanguageLevelId,
                        principalTable: "LanguageLevels",
                        principalColumn: "LanguageLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageCompetences",
                columns: table => new
                {
                    LanguageCompetenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageCompetenceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageCompetenceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCompetenceVideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCompetencePriorityNumber = table.Column<int>(type: "int", nullable: true),
                    LanguageCompetenceType = table.Column<int>(type: "int", nullable: false),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageCompetences", x => x.LanguageCompetenceId);
                    table.ForeignKey(
                        name: "FK_LanguageCompetences_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "ChapterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageCompetences_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId");
                });

            migrationBuilder.CreateTable(
                name: "ChapterResults",
                columns: table => new
                {
                    ChapterResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageLevelResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterResults", x => x.ChapterResultId);
                    table.ForeignKey(
                        name: "FK_ChapterResults_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "ChapterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChapterResults_LanguageLevelResults_LanguageLevelResultId",
                        column: x => x.LanguageLevelResultId,
                        principalTable: "LanguageLevelResults",
                        principalColumn: "LanguageLevelResultId");
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonRequirement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonPriorityNumber = table.Column<int>(type: "int", nullable: true),
                    LessonType = table.Column<int>(type: "int", nullable: false),
                    LessonVideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LanguageCompetenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    TextScript = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_Lessons_LanguageCompetences_LanguageCompetenceId",
                        column: x => x.LanguageCompetenceId,
                        principalTable: "LanguageCompetences",
                        principalColumn: "LanguageCompetenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLanguageLevels",
                columns: table => new
                {
                    UserLanguageLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageCompetenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLanguageLevels", x => x.UserLanguageLevelId);
                    table.ForeignKey(
                        name: "FK_UserLanguageLevels_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentId");
                    table.ForeignKey(
                        name: "FK_UserLanguageLevels_LanguageCompetences_LanguageCompetenceId",
                        column: x => x.LanguageCompetenceId,
                        principalTable: "LanguageCompetences",
                        principalColumn: "LanguageCompetenceId");
                    table.ForeignKey(
                        name: "FK_UserLanguageLevels_LanguageLevels_LanguageLevelId",
                        column: x => x.LanguageLevelId,
                        principalTable: "LanguageLevels",
                        principalColumn: "LanguageLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageCompetenceResults",
                columns: table => new
                {
                    LanguageCompetenceResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    LanguageCompetenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChapterResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageCompetenceResults", x => x.LanguageCompetenceResultId);
                    table.ForeignKey(
                        name: "FK_LanguageCompetenceResults_ChapterResults_ChapterResultId",
                        column: x => x.ChapterResultId,
                        principalTable: "ChapterResults",
                        principalColumn: "ChapterResultId");
                    table.ForeignKey(
                        name: "FK_LanguageCompetenceResults_LanguageCompetences_LanguageCompetenceId",
                        column: x => x.LanguageCompetenceId,
                        principalTable: "LanguageCompetences",
                        principalColumn: "LanguageCompetenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityTags",
                columns: table => new
                {
                    EntityTagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LanguageCompetenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LanguageLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTags", x => x.EntityTagId);
                    table.ForeignKey(
                        name: "FK_EntityTags_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "ChapterId");
                    table.ForeignKey(
                        name: "FK_EntityTags_LanguageCompetences_LanguageCompetenceId",
                        column: x => x.LanguageCompetenceId,
                        principalTable: "LanguageCompetences",
                        principalColumn: "LanguageCompetenceId");
                    table.ForeignKey(
                        name: "FK_EntityTags_LanguageLevels_LanguageLevelId",
                        column: x => x.LanguageLevelId,
                        principalTable: "LanguageLevels",
                        principalColumn: "LanguageLevelId");
                    table.ForeignKey(
                        name: "FK_EntityTags_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId");
                    table.ForeignKey(
                        name: "FK_EntityTags_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId");
                    table.ForeignKey(
                        name: "FK_EntityTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionRequirement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionLearningType = table.Column<int>(type: "int", nullable: false),
                    QuestionImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    QuestionVideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionPriorityNumber = table.Column<int>(type: "int", nullable: true),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId");
                    table.ForeignKey(
                        name: "FK_Questions_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonResults",
                columns: table => new
                {
                    LessonResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageCompetenceResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonResults", x => x.LessonResultId);
                    table.ForeignKey(
                        name: "FK_LessonResults_LanguageCompetenceResults_LanguageCompetenceResultId",
                        column: x => x.LanguageCompetenceResultId,
                        principalTable: "LanguageCompetenceResults",
                        principalColumn: "LanguageCompetenceResultId");
                    table.ForeignKey(
                        name: "FK_LessonResults_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    ChoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChoiceContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.ChoiceId);
                    table.ForeignKey(
                        name: "FK_Choices_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionResults",
                columns: table => new
                {
                    QuestionResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    AudioData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    RecognizedText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    WritingQuestionResult_RecognizedText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionResults", x => x.QuestionResultId);
                    table.ForeignKey(
                        name: "FK_QuestionResults_LessonResults_LessonResultId",
                        column: x => x.LessonResultId,
                        principalTable: "LessonResults",
                        principalColumn: "LessonResultId");
                    table.ForeignKey(
                        name: "FK_QuestionResults_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChapterResults_ChapterId",
                table: "ChapterResults",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterResults_LanguageLevelResultId",
                table: "ChapterResults",
                column: "LanguageLevelResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_LanguageLevelId",
                table: "Chapters",
                column: "LanguageLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Choices_QuestionId",
                table: "Choices",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_LanguageId",
                table: "Enrollments",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_ChapterId",
                table: "EntityTags",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_LanguageCompetenceId",
                table: "EntityTags",
                column: "LanguageCompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_LanguageId",
                table: "EntityTags",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_LanguageLevelId",
                table: "EntityTags",
                column: "LanguageLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_LessonId",
                table: "EntityTags",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_TagId",
                table: "EntityTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageCompetenceResults_ChapterResultId",
                table: "LanguageCompetenceResults",
                column: "ChapterResultId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageCompetenceResults_LanguageCompetenceId",
                table: "LanguageCompetenceResults",
                column: "LanguageCompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageCompetences_ChapterId",
                table: "LanguageCompetences",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageCompetences_LanguageId",
                table: "LanguageCompetences",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageLevelResults_EnrollmentId",
                table: "LanguageLevelResults",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageLevelResults_LanguageLevelId",
                table: "LanguageLevelResults",
                column: "LanguageLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageLevels_LanguageId",
                table: "LanguageLevels",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResults_LanguageCompetenceResultId",
                table: "LessonResults",
                column: "LanguageCompetenceResultId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResults_LessonId",
                table: "LessonResults",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LanguageCompetenceId",
                table: "Lessons",
                column: "LanguageCompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResults_LessonResultId",
                table: "QuestionResults",
                column: "LessonResultId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResults_QuestionId",
                table: "QuestionResults",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_LanguageId",
                table: "Questions",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_LessonId",
                table: "Questions",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_LearningStyleId",
                table: "Tags",
                column: "LearningStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguageLevels_EnrollmentId",
                table: "UserLanguageLevels",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguageLevels_LanguageCompetenceId",
                table: "UserLanguageLevels",
                column: "LanguageCompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguageLevels_LanguageLevelId",
                table: "UserLanguageLevels",
                column: "LanguageLevelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "EntityTags");

            migrationBuilder.DropTable(
                name: "QuestionResults");

            migrationBuilder.DropTable(
                name: "UserLanguageLevels");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "LessonResults");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "LearningStyles");

            migrationBuilder.DropTable(
                name: "LanguageCompetenceResults");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "ChapterResults");

            migrationBuilder.DropTable(
                name: "LanguageCompetences");

            migrationBuilder.DropTable(
                name: "LanguageLevelResults");

            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "LanguageLevels");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}

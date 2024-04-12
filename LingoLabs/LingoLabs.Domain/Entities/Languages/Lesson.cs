using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Lesson : AuditableEntity
    {
        public Guid LessonId { get; protected set; }
        public string LessonTitle { get; protected set; }
        public string? LessonDescription { get; protected set; } = string.Empty; 
        public string? LessonRequirement { get; protected set; } = string.Empty;     
        public string? LessonContent { get; protected set; } = string.Empty;    
        public int? LessonPriorityNumber { get; protected set; }
        public string? LessonVideoLink { get; protected set; } = string.Empty;
        public string? LessonImageData { get; protected set; } = string.Empty;
        public Guid LanguageCompetenceId { get; protected set; }
        public LanguageCompetence? LanguageCompetence { get; set; }
        public List<Question>? LessonQuestions { get; protected set; } = [];
        public List<EntityTag> LessonTags { get; protected set; } = [];
        public Guid ChapterId { get; protected set; }
        public Chapter? Chapter { get; set; }

        protected Lesson(string lessonTitle, Guid chapterId, Guid languageCompetenceId)
        {
            LessonId = Guid.NewGuid();
            LessonTitle = lessonTitle;
            ChapterId = chapterId;
            LanguageCompetenceId = languageCompetenceId;
        }

        public static Result<Lesson> Create(string lessonTitle, Guid chapterId, Guid languageCompetenceId)
        {
            if (string.IsNullOrWhiteSpace(lessonTitle))
                return Result<Lesson>.Failure("LessonName is required");

            if (chapterId == default)
                return Result<Lesson>.Failure("Invalid chapterId");

            if (languageCompetenceId == default)
                return Result<Lesson>.Failure("Invalid languageCompetenceId");

            return Result<Lesson>.Success(new Lesson(lessonTitle, chapterId, languageCompetenceId));
        }

        private static bool IsValidLessonType(LanguageCompetenceType languageCompetenceType)
        {
            return languageCompetenceType == LanguageCompetenceType.Grammar ||
                   languageCompetenceType == LanguageCompetenceType.Listening ||
                   languageCompetenceType == LanguageCompetenceType.Reading ||
                   languageCompetenceType == LanguageCompetenceType.Writing;
        }

        public void UpdateLesson(string lessonTitle, string lessonDescription, string lessonRequirement, string lessonContent, string imageData, string videoLink, int? lessonPriorityNumber)
        {
            if (!string.IsNullOrWhiteSpace(lessonTitle))
                LessonTitle = lessonTitle;
            if (!string.IsNullOrWhiteSpace(lessonDescription))
                LessonDescription = lessonDescription;
            if (!string.IsNullOrWhiteSpace(lessonRequirement))
                LessonRequirement = lessonRequirement;
            if (!string.IsNullOrWhiteSpace(lessonContent))
                LessonContent = lessonContent;
            if (!string.IsNullOrWhiteSpace(imageData))
                LessonImageData = imageData;
            if (!string.IsNullOrWhiteSpace(videoLink))
                LessonVideoLink = videoLink;

            LessonPriorityNumber = lessonPriorityNumber;
        }

        public void UpdateQuestions(List<Question> questions)
        {
            LessonQuestions = questions;
        }
    }
}

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
        public LanguageCompetenceType LessonType { get; protected set; }
        public string? LessonVideoLink { get; protected set; } = string.Empty;
        public byte[]? LessonImageData { get; protected set; }
        public List<Question>? LessonQuestions { get; protected set; } = [];
        public List<Tag > LessonTags { get; protected set; } = [];
        public Guid LanguageCompetenceId { get; protected set; }
        public LanguageCompetence? LanguageCompetence { get; set; }

        protected Lesson(string lessonTitle, LanguageCompetenceType lessonType, Guid languageCompetenceId)
        {
            LessonId = Guid.NewGuid();
            LessonTitle = lessonTitle;
            LessonType = lessonType;
            LanguageCompetenceId = languageCompetenceId;

        }

        public static Result<Lesson> Create(string lessonTitle, LanguageCompetenceType lessonType, Guid languageCompetenceId)
        {
            if (string.IsNullOrWhiteSpace(lessonTitle))
                return Result<Lesson>.Failure("LessonName is required");

            if (!IsValidLessonType(lessonType))
                return Result<Lesson>.Failure("Invalid LessonType");

            if (languageCompetenceId == default)
                return Result<Lesson>.Failure("Invalid LanguageCompetenceId");

            return Result<Lesson>.Success(new Lesson(lessonTitle, lessonType, languageCompetenceId));
        }

        private static bool IsValidLessonType(LanguageCompetenceType languageCompetenceType)
        {
            return languageCompetenceType == LanguageCompetenceType.Grammar ||
                   languageCompetenceType == LanguageCompetenceType.Listening ||
                   languageCompetenceType == LanguageCompetenceType.Reading ||
                   languageCompetenceType == LanguageCompetenceType.Writing;
        }

        public void AttachDescription(string lessonDescription)
        {
            if (!string.IsNullOrWhiteSpace(lessonDescription))
                LessonDescription = lessonDescription;
        }

        public void AttachRequirement(string lessonRequirement)
        {
            if (!string.IsNullOrWhiteSpace(lessonRequirement))
                LessonRequirement = lessonRequirement;
        }

        public void AttachContent(string lessonContent)
        {
            if (!string.IsNullOrWhiteSpace(lessonContent))
                LessonContent = lessonContent;
        }

        public void AttachQuestion(Question question)
        {
            if (question != null)
            {
                if (LessonQuestions == null)
                    LessonQuestions = new List<Question> { question };
                else
                    LessonQuestions.Add(question);
            }
        }

        public void AttachImageData(byte[] imageData)
        {
            if (imageData != null && imageData.Length > 0)
            {
                LessonImageData = imageData;
            }
        }

        public void AttachVideoLink(string videoLink)
        {
            if (!string.IsNullOrWhiteSpace(videoLink))
                LessonVideoLink = videoLink;
        }

        public void AttachTag(Tag tag)
        {
            if (tag != null)
            {
                if (LessonTags == null)
                    LessonTags = new List<Tag> { tag };
                else
                    LessonTags.Add(tag);
            }
        }

        public void UpdateLesson(string lessonTitle, string lessonDescription, string lessonRequirement, string lessonContent, byte[] imageData, string videoLink, int? lessonPriorityNumber)
        {
            if (!string.IsNullOrWhiteSpace(lessonTitle))
                LessonTitle = lessonTitle;
            if (!string.IsNullOrWhiteSpace(lessonDescription))
                LessonDescription = lessonDescription;
            if (!string.IsNullOrWhiteSpace(lessonRequirement))
                LessonRequirement = lessonRequirement;
            if (!string.IsNullOrWhiteSpace(lessonContent))
                LessonContent = lessonContent;
            if (imageData != null && imageData.Length > 0)
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

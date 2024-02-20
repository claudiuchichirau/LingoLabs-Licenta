using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class LanguageCompetence : AuditableEntity
    {
        public Guid LanguageCompetenceId { get; private set; }
        public string LanguageCompetenceName { get; private set; }
        public string? LanguageCompetenceDescription { get; private set; } = string.Empty;
        public string? LanguageCompetenceVideoLink { get; private set; } = string.Empty;
        public LanguageCompetenceType LanguageCompetenceType { get; private set; }
        public List<Lesson>? Lessons { get; private set; } = new();
        public List<Tag>? LearningCompetenceKeyWords { get; private set; } = new();
        public Guid ChapterId { get; private set; }
        public Chapter? Chapter { get; set; }
        public Guid LanguageId { get; private set; }
        public Language? Language { get; set; }

        private LanguageCompetence(string languageCompetenceName, LanguageCompetenceType languageCompetenceType, Guid chapterId, Guid languageId)
        {
            LanguageCompetenceId = Guid.NewGuid();
            LanguageCompetenceName = languageCompetenceName;
            LanguageCompetenceType = languageCompetenceType;
            ChapterId = chapterId;
            LanguageId = languageId;
        }

        public static Result<LanguageCompetence> Create(string languageCompetenceName, LanguageCompetenceType languageCompetenceType, Guid chapterId, Guid languageId)
        {
            if (string.IsNullOrWhiteSpace(languageCompetenceName))
                return Result<LanguageCompetence>.Failure("LanguageCompetenceName is required");

            if (!IsValidLanguageCompetenceType(languageCompetenceType))
                return Result<LanguageCompetence>.Failure("Invalid LanguageCompetenceType");

            if (chapterId == default)
                return Result<LanguageCompetence>.Failure("ChapterId is required");

            if (languageId == default)
                return Result<LanguageCompetence>.Failure("LanguageId is required");

            return Result<LanguageCompetence>.Success(new LanguageCompetence(languageCompetenceName, languageCompetenceType, chapterId, languageId));
        }

        private static bool IsValidLanguageCompetenceType(LanguageCompetenceType languageCompetenceType)
        {
            return languageCompetenceType == LanguageCompetenceType.Grammar ||
                   languageCompetenceType == LanguageCompetenceType.Listening ||
                   languageCompetenceType == LanguageCompetenceType.Reading ||
                   languageCompetenceType == LanguageCompetenceType.Writing;
        }

        public void AttachDescription(string languageCompetenceDescription)
        {
            if (!string.IsNullOrWhiteSpace(languageCompetenceDescription))
                LanguageCompetenceDescription = languageCompetenceDescription;
        }

        public void AttachLesson(Lesson lesson)
        {
            if (lesson != null)
            {
                if (lesson.LessonType == LanguageCompetenceType)    // This is to ensure that the lesson type matches the language competence type
                {
                    if (Lessons == null)
                        Lessons = new List<Lesson> { lesson };
                    else
                        Lessons.Add(lesson);
                }
            }
        }

        public void AttachKeyWord(Tag tag)
        {
            if (tag != null)
            {
                if (LearningCompetenceKeyWords == null)
                    LearningCompetenceKeyWords = new List<Tag> { tag };
                else
                    LearningCompetenceKeyWords.Add(tag);
            }
        }

        public void AttachVideoLink(string languageCompetenceVideoLink)
        {
            if (!string.IsNullOrWhiteSpace(languageCompetenceVideoLink))
                LanguageCompetenceVideoLink = languageCompetenceVideoLink;
        }

        public void UpdateLanguageCompetence(string languageCompetenceName, string languageCompetenceDescription, LanguageCompetenceType languageCompetenceType, string languageCompetenceVideoLink)
        {
            if (!string.IsNullOrWhiteSpace(languageCompetenceName))
                LanguageCompetenceName = languageCompetenceName;
            if (!string.IsNullOrWhiteSpace(languageCompetenceDescription))
                LanguageCompetenceDescription = languageCompetenceDescription;
            if (IsValidLanguageCompetenceType(languageCompetenceType))
                LanguageCompetenceType = languageCompetenceType;
            if (!string.IsNullOrWhiteSpace(languageCompetenceVideoLink))
                LanguageCompetenceVideoLink = languageCompetenceVideoLink;
        }
    }
}

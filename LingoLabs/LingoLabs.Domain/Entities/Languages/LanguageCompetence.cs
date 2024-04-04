using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;

namespace LingoLabs.Domain.Entities.Languages
{
    public class LanguageCompetence : AuditableEntity
    {
        public Guid LanguageCompetenceId { get; private set; }
        public string LanguageCompetenceName { get; private set; }
        public string? LanguageCompetenceDescription { get; private set; } = string.Empty;
        public string? LanguageCompetenceVideoLink { get; private set; } = string.Empty;
        public int? LanguageCompetencePriorityNumber { get; private set; }
        public LanguageCompetenceType LanguageCompetenceType { get; private set; }
        public List<Lesson>? LanguageCompetenceLessons { get; private set; } = [];
        public List<EntityTag>? LearningCompetenceTags { get; private set; } = [];
        public Guid LanguageId { get; private set; }
        public Language? Language { get; set; }
        public List<UserLanguageLevel>? UserLanguageLevels { get; private set; } = [];

        private LanguageCompetence(string languageCompetenceName, LanguageCompetenceType languageCompetenceType, Guid languageId)
        {
            LanguageCompetenceId = Guid.NewGuid();
            LanguageCompetenceName = languageCompetenceName;
            LanguageCompetenceType = languageCompetenceType;
            LanguageId = languageId;
        }

        public static Result<LanguageCompetence> Create(string languageCompetenceName, LanguageCompetenceType languageCompetenceType, Guid languageId)
        {
            if (string.IsNullOrWhiteSpace(languageCompetenceName))
                return Result<LanguageCompetence>.Failure("LanguageCompetenceName is required");

            if (!IsValidLanguageCompetenceType(languageCompetenceType))
                return Result<LanguageCompetence>.Failure("Invalid LanguageCompetenceType");

            if (languageId == default)
                return Result<LanguageCompetence>.Failure("LanguageId is required");

            return Result<LanguageCompetence>.Success(new LanguageCompetence(languageCompetenceName, languageCompetenceType, languageId));
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

        public void AttachKeyWord(EntityTag tag)
        {
            if (tag != null)
            {
                if (LearningCompetenceTags == null)
                    LearningCompetenceTags = new List<EntityTag> { tag };
                else
                    LearningCompetenceTags.Add(tag);
            }
        }

        public void AttachVideoLink(string languageCompetenceVideoLink)
        {
            if (!string.IsNullOrWhiteSpace(languageCompetenceVideoLink))
                LanguageCompetenceVideoLink = languageCompetenceVideoLink;
        }

        public void UpdateLanguageCompetence(string languageCompetenceDescription, string languageCompetenceVideoLink, int? languageCompetencePriorityNumber)
        {
            if (!string.IsNullOrWhiteSpace(languageCompetenceDescription))
                LanguageCompetenceDescription = languageCompetenceDescription;
            if (!string.IsNullOrWhiteSpace(languageCompetenceVideoLink))
                LanguageCompetenceVideoLink = languageCompetenceVideoLink;
            LanguageCompetencePriorityNumber = languageCompetencePriorityNumber;
        }
    }
}

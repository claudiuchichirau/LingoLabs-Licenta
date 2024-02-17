using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class Enrollment : AuditableEntity
    {
        public Guid EnrollmentId { get; private set; }
        public Guid UserId { get; private set; }
        public List<LanguageLevelResult>? LanguageLevelResults { get; private set; } = new();
        public List<UserLanguageLevel>? UserLanguageLevels { get; private set; }
        public Guid LanguageId { get; private set; }
        public Language? Language { get; set; }

        private Enrollment(Guid userId, Guid languageId)
        {
            EnrollmentId = Guid.NewGuid();
            UserId = userId;
            LanguageId = languageId;
        }

        public static Result<Enrollment> Create(Guid userId, Guid languageId)
        {
            if(userId == default)
                return Result<Enrollment>.Failure("UserId is required");
            if(languageId == default)
                return Result<Enrollment>.Failure("LanguageId is required");
            return Result<Enrollment>.Success(new Enrollment(userId, languageId));
        }

        public void AttachLanguageLevelResult(LanguageLevelResult languageLevelResult)
        {
            if(languageLevelResult != null)
            {
                if(LanguageLevelResults == null)
                    LanguageLevelResults = new List<LanguageLevelResult> { languageLevelResult };
                else   
                    LanguageLevelResults.Add(languageLevelResult);
            }
        }

        public void AttachUserLanguageLevel(UserLanguageLevel userLanguageLevel)
        {
            if(userLanguageLevel != null)
            {
                if(UserLanguageLevels == null)
                    UserLanguageLevels = new List<UserLanguageLevel> { userLanguageLevel };
                else   
                    UserLanguageLevels.Add(userLanguageLevel);
            }
        }

    }
}
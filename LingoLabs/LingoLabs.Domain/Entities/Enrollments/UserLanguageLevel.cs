using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class UserLanguageLevel : AuditableEntity
    {
        public Guid UserLanguageLevelId { get; private set; }
        public Guid EnrollmentId { get; private set; }
        public Enrollment? Enrollment { get; set; }
        public Guid LanguageCompetenceId { get; private set; }
        public LanguageCompetence? LanguageCompetence { get; set; }
        public Guid LanguageLevelId { get; private set; }
        public LanguageLevel? LanguageLevel { get; set; }

        private UserLanguageLevel(Guid enrollmentId, Guid languageCompetenceId, Guid languageLevelId)
        {
            UserLanguageLevelId = Guid.NewGuid();
            EnrollmentId = enrollmentId;
            LanguageCompetenceId = languageCompetenceId;
            LanguageLevelId = languageLevelId;
        }

        public static Result<UserLanguageLevel> Create(Guid enrollmentId, Guid languageCompetenceId, Guid languageLevelId)
        {
            if(enrollmentId == default)
                return Result<UserLanguageLevel>.Failure("EnrollmentId is required");
            if(languageCompetenceId == default)
                return Result<UserLanguageLevel>.Failure("LanguageCompetenceId is required");
            if(languageLevelId == default)
                return Result<UserLanguageLevel>.Failure("LanguageLevelId is required");
            return Result<UserLanguageLevel>.Success(new UserLanguageLevel(enrollmentId, languageCompetenceId, languageLevelId));
        }

        public void Update(Guid languageLevelId)
        {
            if(languageLevelId != default)
                LanguageLevelId = languageLevelId;
        }
    }
}
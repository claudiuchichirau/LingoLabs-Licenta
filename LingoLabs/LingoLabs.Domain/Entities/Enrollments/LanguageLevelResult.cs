using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class LanguageLevelResult : AuditableEntity
    {
        public Guid LanguageLevelResultId { get; private set; }
        public List<ChapterResult>? ChapterResults { get; private set; } = new();
        public bool IsCompleted { get; private set; }
        public Guid LanguageLevelId { get; private set; }
        public LanguageLevel? LanguageLevel { get; set; }
        public Guid EnrollmentId { get; private set; }
        public Enrollment? Enrollment { get; set; }

        private LanguageLevelResult(Guid languageLevelId, Guid enrollmentId)
        {
            LanguageLevelResultId = Guid.NewGuid();
            LanguageLevelId = languageLevelId;
            EnrollmentId = enrollmentId;
            IsCompleted = false;
        }

        public static Result<LanguageLevelResult> Create(Guid languageLevelId, Guid enrollmentId)
        {
            if(languageLevelId == default)
                return Result<LanguageLevelResult>.Failure("LanguageLevelId is required");
            if(enrollmentId == default)
                return Result<LanguageLevelResult>.Failure("EnrollmentId is required");
            return Result<LanguageLevelResult>.Success(new LanguageLevelResult(languageLevelId, enrollmentId));
        }

        public void AttachChapterResult(ChapterResult chapterResult)
        {
            if(chapterResult != null)
            {
                if(ChapterResults == null)
                    ChapterResults = new List<ChapterResult> { chapterResult };
                else   
                    ChapterResults.Add(chapterResult);
            }
        }

        public void UpdateLanguageLevelResult(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }
    }
}

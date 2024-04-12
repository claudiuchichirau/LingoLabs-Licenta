using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class LanguageCompetenceResult : AuditableEntity
    {
        public Guid LanguageCompetenceResultId { get; private set; }
        public bool IsCompleted { get; private set; }
        public List<LessonResult>? LessonsResults { get; private set; } = [];
        public Guid LanguageCompetenceId { get; private set; }
        public LanguageCompetence? LanguageCompetence { get; set; }
        public Guid EnrollmentId { get; private set; }
        public Enrollment? Enrollment { get; set; }

        private LanguageCompetenceResult(Guid languageCompetenceId, Guid enrollmentId, bool isCompleted)
        {
            LanguageCompetenceResultId = Guid.NewGuid();
            LanguageCompetenceId = languageCompetenceId;
            EnrollmentId = enrollmentId;
            IsCompleted = false;
        }

        public static Result<LanguageCompetenceResult> Create( Guid languageCompetenceId, Guid enrollmentId, bool isCompleted)
        {
            if(languageCompetenceId == default)
                return Result<LanguageCompetenceResult>.Failure("LanguageCompetenceId is required");
            if(enrollmentId == default)
                return Result<LanguageCompetenceResult>.Failure("ChapterResultId is required");
            return Result<LanguageCompetenceResult>.Success(new LanguageCompetenceResult(languageCompetenceId, enrollmentId, isCompleted));
        }

        public void AttachLessonResult(LessonResult lessonResult)
        {
            if(lessonResult != null)
            {
                if(LessonsResults == null)
                    LessonsResults = new List<LessonResult> { lessonResult };
                else   
                    LessonsResults.Add(lessonResult);
            }
        }

        public void UpdateLanguageCompetenceResult(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }
    }
}
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class LanguageCompetenceResult : AuditableEntity
    {
        public Guid LanguageCompetenceResultId { get; private set; }
        public bool? IsCompleted { get; private set; }
        public List<LessonResult>? LessonsResults { get; private set; } = new();
        public Guid LanguageCompetenceId { get; private set; }
        public LanguageCompetence? LanguageCompetence { get; set; }
        public Guid ChapterResultId { get; private set; }
        public ChapterResult? ChapterResult { get; set; }

        private LanguageCompetenceResult(Guid languageCompetenceId, Guid chapterResultId)
        {
            LanguageCompetenceResultId = Guid.NewGuid();
            LanguageCompetenceId = languageCompetenceId;
            ChapterResultId = chapterResultId;
            IsCompleted = false;
        }

        public static Result<LanguageCompetenceResult> Create( Guid languageCompetenceId, Guid chapterResultId)
        {
            if(languageCompetenceId == default)
                return Result<LanguageCompetenceResult>.Failure("LanguageCompetenceId is required");
            if(chapterResultId == default)
                return Result<LanguageCompetenceResult>.Failure("ChapterResultId is required");
            return Result<LanguageCompetenceResult>.Success(new LanguageCompetenceResult(languageCompetenceId, chapterResultId));
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

        public void MakeCompletedLanguageCompetence()
        {
            IsCompleted = true;
        }
    }
}
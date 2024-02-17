using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class LessonResult : AuditableEntity
    {
        public Guid LessonResultId { get; private set; }
        public bool? IsCompleted { get; private set; }
        public List<QuestionResult>? QuestionResults { get; private set; } = new();
        public Guid LessonId { get; private set; }
        public Lesson? Lesson { get; set; }
        public Guid LanguageCompetenceResultId { get; private set; }
        public LanguageCompetenceResult? LanguageCompetenceResult { get; set; }

        private LessonResult(Guid lessonId, Guid languageCompetenceResultId)
        {
            LessonResultId = Guid.NewGuid();
            LessonId = lessonId;
            LanguageCompetenceResultId = languageCompetenceResultId;
        }

        public static Result<LessonResult> Create(Guid lessonId, Guid languageCompetenceResultId)
        {
            if(lessonId == default)
                return Result<LessonResult>.Failure("LessonId is required");
            if(languageCompetenceResultId == default)
                return Result<LessonResult>.Failure("LanguageCompetenceResultId is required");
            return Result<LessonResult>.Success(new LessonResult(lessonId, languageCompetenceResultId));
        }

        public void AttachQuestionResult(QuestionResult questionResult)
        {
            if(questionResult != null)
            {
                if(QuestionResults == null)
                    QuestionResults = new List<QuestionResult> { questionResult };
                else
                    QuestionResults.Add(questionResult);
            }
        }

        public void MakeCompletedLesson()
        {
            IsCompleted = true;
        }
    }
}

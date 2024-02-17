using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class QuestionResult : AuditableEntity
    {
        public Guid QuestionResultId { get; private set; }
        public bool IsCorrect { get; private set; }
        public Guid QuestionId { get; private set; }
        public Question? Question { get; set; }
        public Guid LessonResultId { get; private set; }
        public LessonResult? LessonResult { get; set; }

        protected QuestionResult(Guid questionId, Guid lessonResultId, bool isCorrect)
        {
            QuestionResultId = Guid.NewGuid();
            QuestionId = questionId;
            IsCorrect = isCorrect;
            LessonResultId = lessonResultId;
        }

        public static Result<QuestionResult> Create(Guid questionId, Guid lessonResultId, bool isCorrect)
        {
            if(questionId == default)
                return Result<QuestionResult>.Failure("QuestionId is required");
            if(lessonResultId == default)
                return Result<QuestionResult>.Failure("LessonResultId is required");
            return Result<QuestionResult>.Success(new QuestionResult(questionId, lessonResultId, isCorrect));
        }
    }
}

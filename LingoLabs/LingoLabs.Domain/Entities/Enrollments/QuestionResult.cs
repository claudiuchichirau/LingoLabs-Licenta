using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class QuestionResult : AuditableEntity
    {
        public Guid QuestionResultId { get; protected set; }
        public bool IsCorrect { get; protected set; }
        public Guid QuestionId { get; protected set; }
        public Question? Question { get; set; }
        public Guid LessonResultId { get; protected set; }
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

        public void UpdateQuestionResult(bool isCorrect)
        {
            if (isCorrect != default)
                IsCorrect = isCorrect;
        }
    }
}

using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class WritingQuestionResult : QuestionResult
    {
        public byte[]? ImageData { get; private set; }
        public string? RecognizedText { get; private set; }

        private WritingQuestionResult(Guid questionId, Guid lessonResultId, bool isCorrect) : base(questionId, lessonResultId, isCorrect)
        {
        }

        public static Result<WritingQuestionResult> Create(Guid questionId, Guid lessonResultId, bool isCorrect)
        {
            if (questionId == default)
                return Result<WritingQuestionResult>.Failure("QuestionId is required");
            if (lessonResultId == default)
                return Result<WritingQuestionResult>.Failure("LessonResultId is required");
            return Result<WritingQuestionResult>.Success(new WritingQuestionResult(questionId, lessonResultId, isCorrect));
        }

        public void AttachImageData(byte[] imageData)
        {
            ImageData = imageData;
        }

        public void AttachRecognizedText(string recognizedText)
        {
            RecognizedText = recognizedText;
        }
    }
}

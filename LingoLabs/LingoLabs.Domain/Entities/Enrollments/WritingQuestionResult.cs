using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class WritingQuestionResult : QuestionResult
    {
        public byte[]? ImageData { get; private set; }
        public string? RecognizedText { get; private set; }

        private WritingQuestionResult(Guid questionId, Guid userId, bool isCorrect) : base(questionId, userId, isCorrect)
        {
        }

        public static Result<WritingQuestionResult> Create(Guid questionId, Guid userId, bool isCorrect)
        {
            if (questionId == default)
                return Result<WritingQuestionResult>.Failure("QuestionId is required");
            if (userId == default)
                return Result<WritingQuestionResult>.Failure("UserId is required");
            return Result<WritingQuestionResult>.Success(new WritingQuestionResult(questionId, userId, isCorrect));
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

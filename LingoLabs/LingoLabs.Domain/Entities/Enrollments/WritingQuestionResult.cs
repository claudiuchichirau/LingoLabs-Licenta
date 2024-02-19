using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class WritingQuestionResult : QuestionResult
    {
        public byte[]? ImageData { get; private set; }
        public string? RecognizedText { get; private set; }

        private WritingQuestionResult(Guid questionId, Guid lessonResultId, bool isCorrect, byte[] imageData) : base(questionId, lessonResultId, isCorrect)
        {
            ImageData = imageData;
        }

        private WritingQuestionResult(Guid questionId, Guid lessonResultId, bool isCorrect, string recognizedText) : base(questionId, lessonResultId, isCorrect)
        {
            RecognizedText = recognizedText;
        }

        public static Result<WritingQuestionResult> CreateFromImage(Guid questionId, Guid lessonResultId, bool isCorrect, byte[] imageData)
        {
            if (questionId == default)
                return Result<WritingQuestionResult>.Failure("QuestionId is required");
            if (lessonResultId == default)
                return Result<WritingQuestionResult>.Failure("LessonResultId is required");
            if (imageData == default)
                return Result<WritingQuestionResult>.Failure("ImageData is required");
            return Result<WritingQuestionResult>.Success(new WritingQuestionResult(questionId, lessonResultId, isCorrect, imageData));
        }

        public static Result<WritingQuestionResult> CreateFromText(Guid questionId, Guid lessonResultId, bool isCorrect, string recognizedText)
        {
            if (questionId == default)
                return Result<WritingQuestionResult>.Failure("QuestionId is required");
            if (lessonResultId == default)
                return Result<WritingQuestionResult>.Failure("LessonResultId is required");
            if (recognizedText == null)
                return Result<WritingQuestionResult>.Failure("RecognizedText is required");
            return Result<WritingQuestionResult>.Success(new WritingQuestionResult(questionId, lessonResultId, isCorrect, recognizedText));
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

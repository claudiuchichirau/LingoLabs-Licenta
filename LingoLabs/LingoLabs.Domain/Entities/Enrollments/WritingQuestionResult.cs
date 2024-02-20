using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class WritingQuestionResult : QuestionResult
    {
        public byte[]? ImageData { get; private set; }
        public string? RecognizedText { get; private set; }

        private WritingQuestionResult(Guid questionId, Guid lessonResultId, bool isCorrect, byte[]? imageData, string? recognizedText) : base(questionId, lessonResultId, isCorrect)
        {
            QuestionResultId = Guid.NewGuid();
            QuestionId = questionId;
            LessonResultId = lessonResultId;
            IsCorrect = isCorrect;
            RecognizedText = recognizedText;
            ImageData = imageData;
        }

        public static Result<WritingQuestionResult> Create(Guid questionId, Guid lessonResultId, bool isCorrect, byte[]? imageData, string? recognizedText)
        {
            if (questionId == default)
                return Result<WritingQuestionResult>.Failure("QuestionId is required");
            if (lessonResultId == default)
                return Result<WritingQuestionResult>.Failure("LessonResultId is required");
            if (imageData == default && recognizedText == default)
                return Result<WritingQuestionResult>.Failure("ImageData or RecognizedText is required");
            if(imageData != default && recognizedText != default)
                return Result<WritingQuestionResult>.Failure("ImageData and RecognizedText cannot be both provided");
            return Result<WritingQuestionResult>.Success(new WritingQuestionResult(questionId, lessonResultId, isCorrect, imageData, recognizedText));
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

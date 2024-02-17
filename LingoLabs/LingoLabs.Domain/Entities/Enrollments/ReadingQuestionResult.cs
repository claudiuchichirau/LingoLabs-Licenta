using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class ReadingQuestionResult : QuestionResult
    {
        public byte[]? AudioData { get; private set; }
        public string? RecognizedText { get; private set; }

        private ReadingQuestionResult(Guid questionId, Guid userId, bool isCorrect) : base(questionId, userId, isCorrect)
        {
        }

        public static Result<ReadingQuestionResult> Create(Guid questionId, Guid userId, bool isCorrect)
        {
            if (questionId == default)
                return Result<ReadingQuestionResult>.Failure("QuestionId is required");
            if (userId == default)
                return Result<ReadingQuestionResult>.Failure("UserId is required");
            return Result<ReadingQuestionResult>.Success(new ReadingQuestionResult(questionId, userId, isCorrect));
        }

        public void AttachAudioData(byte[] audioData)
        {
            AudioData = audioData;
        }

        public void AttachRecognizedText(string recognizedText)
        {
            RecognizedText = recognizedText;
        }
    }
}

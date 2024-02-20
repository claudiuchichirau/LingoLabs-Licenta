using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class ReadingQuestionResult : QuestionResult
    {
        public byte[] AudioData { get; private set; }
        public string? RecognizedText { get; private set; }

        private ReadingQuestionResult(Guid questionId, Guid lessonResultId, bool isCorrect, byte[] audioData) : base(questionId, lessonResultId, isCorrect)
        {
            QuestionResultId = Guid.NewGuid();
            QuestionId = questionId;
            LessonResultId = lessonResultId;
            IsCorrect = isCorrect;
            AudioData = audioData;
        }

        public static Result<ReadingQuestionResult> Create(Guid questionId, Guid lessonResultId, bool isCorrect, byte[] audioData)
        {
            if (questionId == default)
                return Result<ReadingQuestionResult>.Failure("QuestionId is required");
            if (lessonResultId == default)
                return Result<ReadingQuestionResult>.Failure("LessonResultId is required");
            if (isCorrect == default)
                return Result<ReadingQuestionResult>.Failure("IsCorrect is required");
            if (audioData == null || audioData.Length == 0)
                return Result<ReadingQuestionResult>.Failure("AudioData is required");
            return Result<ReadingQuestionResult>.Success(new ReadingQuestionResult(questionId, lessonResultId, isCorrect, audioData));
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

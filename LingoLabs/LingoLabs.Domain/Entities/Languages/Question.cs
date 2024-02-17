using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Question : AuditableEntity
    {
        public Guid QuestionId { get; private set; }
        public string QuestionRequirement { get; private set; }
        public LearningType QuestionLearningType { get; private set; }
        public List<Choice>? QuestionChoices { get; private set; } = new();
        public byte[]? QuestionImageData { get; private set; }
        public string? QuestionVideoLink { get; private set; } = string.Empty;
        public Guid LessonId { get; private set; }
        public Lesson? Lesson { get; set; }

        protected Question(string questionRequirement, LearningType questionLearningType)
        {
            QuestionId = Guid.NewGuid();
            QuestionRequirement = questionRequirement;
            QuestionLearningType = questionLearningType;
        }

        public static Result<Question> Create(string questionRequirement, LearningType questionLearningType)
        {
            if (string.IsNullOrWhiteSpace(questionRequirement))
                return Result<Question>.Failure("QuestionRequirement is required");

            if (!IsValidLearningType(questionLearningType))
                return Result<Question>.Failure("Invalid LearningType");

            return Result<Question>.Success(new Question(questionRequirement, questionLearningType));
        }

        public static bool IsValidLearningType(LearningType learningType)
        {
            return learningType == LearningType.Auditory ||
                   learningType == LearningType.Visual ||
                   learningType == LearningType.Kinesthetic ||
                   learningType == LearningType.Logical;
        }

        public void AttachChoices(Choice choice)
        {
            if (choice != null)
            {
                if (QuestionChoices == null)
                    QuestionChoices = new List<Choice> { choice };
                else
                    QuestionChoices.Add(choice);
            }
        }

        public void AttachImageData(byte[] imageData)
        {
            if (imageData != null && imageData.Length > 0)
            {
                QuestionImageData = imageData;
            }
        }

        public void AttachVideoLink(string videoLink)
        {
            if (!string.IsNullOrWhiteSpace(videoLink))
                QuestionVideoLink = videoLink;
        }

        public void UpdateLesson(string questionRequirement, LearningType questionLearningType, byte[] imageData, string videoLink)
        {
            if (!string.IsNullOrWhiteSpace(questionRequirement))
                QuestionRequirement = questionRequirement;

            if (IsValidLearningType(questionLearningType))
                QuestionLearningType = questionLearningType;

            if (imageData != null && imageData.Length > 0)
                QuestionImageData = imageData;

            if (!string.IsNullOrWhiteSpace(videoLink))
                QuestionVideoLink = videoLink;
        }
    }
}

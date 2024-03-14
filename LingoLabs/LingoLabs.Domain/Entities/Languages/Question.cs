using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Question : AuditableEntity
    {
        public Guid QuestionId { get; protected set; }
        public string QuestionRequirement { get; protected set; }
        public LearningType QuestionLearningType { get; protected set; }
        public List<Choice>? QuestionChoices { get; protected set; } = [];
        public byte[]? QuestionImageData { get; private set; }
        public string? QuestionVideoLink { get; private set; } = string.Empty;
        public int? QuestionPriorityNumber { get; private set; }
        public Guid LessonId { get; protected set; }
        public Lesson? Lesson { get; set; }
        public Guid? LanguageId { get; protected set; }
        public Language? Language { get; set; }

        protected Question(string questionRequirement, LearningType questionLearningType, Guid lessonId)
        {
            QuestionId = Guid.NewGuid();
            QuestionRequirement = questionRequirement;
            QuestionLearningType = questionLearningType;
            LessonId = lessonId;
        }

        public static Result<Question> Create(string questionRequirement, LearningType questionLearningType, Guid lessonId)
        {
            if (string.IsNullOrWhiteSpace(questionRequirement))
                return Result<Question>.Failure("QuestionRequirement is required");

            if (!IsValidLearningType(questionLearningType))
                return Result<Question>.Failure("Invalid LearningType");

            if (lessonId == default)
                return Result<Question>.Failure("LessonId is required");

            return Result<Question>.Success(new Question(questionRequirement, questionLearningType, lessonId));
        }

        public static bool IsValidLearningType(LearningType learningType)
        {
            return learningType == LearningType.Auditory ||
                   learningType == LearningType.Visual ||
                   learningType == LearningType.Kinesthetic ||
                   learningType == LearningType.Logical;
        }

        public void UpdateQuestion(string questionRequirement, LearningType questionLearningType, byte[] imageData, string videoLink, Guid languageId, int? questionPriorityNumber)
        {
            if (!string.IsNullOrWhiteSpace(questionRequirement))
                QuestionRequirement = questionRequirement;

            if (IsValidLearningType(questionLearningType))
                QuestionLearningType = questionLearningType;

            if (imageData != null && imageData.Length > 0)
                QuestionImageData = imageData;

            if (!string.IsNullOrWhiteSpace(videoLink))
                QuestionVideoLink = videoLink;

            if (languageId != default)
                LanguageId = languageId;

            QuestionPriorityNumber = questionPriorityNumber;
        }

        public void UpdateQuestionLanguageId(Guid languageId)
        {
            LanguageId = languageId;
        }

        public void RemoveQuestionLanguageId()
        {
            LanguageId = null;
        }

        public void UpdateQuestionRequirement(string questionRequirement)
        {
            QuestionRequirement = questionRequirement;
        }

        public void UpdateQuestionLearningType(LearningType questionLearningType)
        {
            if (IsValidLearningType(questionLearningType))
                QuestionLearningType = questionLearningType;
        }

        public void UpdateQuestionImageData(byte[] imageData)
        {
            if (imageData != null && imageData.Length > 0)
                QuestionImageData = imageData;
        }

        public void UpdateQuestionVideoLink(string videoLink)
        {
            if (!string.IsNullOrWhiteSpace(videoLink))
                QuestionVideoLink = videoLink;
        }
    }
}

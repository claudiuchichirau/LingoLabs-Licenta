using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Question : AuditableEntity
    {
        public Guid QuestionId { get; protected set; }
        public string QuestionRequirement { get; protected set; }
        public QuestionType QuestionType { get; protected set; }
        public List<Choice>? Choices { get; protected set; } = [];
        public string? QuestionImageData { get; private set; } = string.Empty;
        public string? QuestionVideoLink { get; private set; } = string.Empty;
        public int? QuestionPriorityNumber { get; private set; }
        public Guid LessonId { get; protected set; }
        public Lesson? Lesson { get; set; }
        public Guid? LanguageId { get; protected set; }
        public Language? Language { get; set; }

        protected Question(string questionRequirement, QuestionType questionType, Guid lessonId)
        {
            QuestionId = Guid.NewGuid();
            QuestionRequirement = questionRequirement;
            QuestionType = questionType;
            LessonId = lessonId;
        }

        public static Result<Question> Create(string questionRequirement, QuestionType questionType, Guid lessonId)
        {
            if (string.IsNullOrWhiteSpace(questionRequirement))
                return Result<Question>.Failure("QuestionRequirement is required");

            if (!IsValidQuestionType(questionType))
                return Result<Question>.Failure("Invalid LearningType");

            if (lessonId == default)
                return Result<Question>.Failure("LessonId is required");

            return Result<Question>.Success(new Question(questionRequirement, questionType, lessonId));
        }

        public static bool IsValidQuestionType(QuestionType questionType)
        {
            return questionType == QuestionType.TrueFalse ||
                   questionType == QuestionType.MissingWord ||
                   questionType == QuestionType.MultipleChoice;
        }

        public void UpdateQuestion(string questionRequirement, string imageData, string videoLink, Guid? languageId, int? questionPriorityNumber)
        {
            if (!string.IsNullOrWhiteSpace(questionRequirement))
                QuestionRequirement = questionRequirement;

            if (!string.IsNullOrWhiteSpace(imageData))
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

        public void UpdateQuestionType(QuestionType questionType)
        {
            if (IsValidQuestionType(questionType))
                QuestionType = questionType;
        }

        public void UpdateQuestionImageData(string imageData)
        {
            if (!string.IsNullOrWhiteSpace(imageData))
                QuestionImageData = imageData;
        }

        public void UpdateQuestionVideoLink(string videoLink)
        {
            if (!string.IsNullOrWhiteSpace(videoLink))
                QuestionVideoLink = videoLink;
        }
    }
}

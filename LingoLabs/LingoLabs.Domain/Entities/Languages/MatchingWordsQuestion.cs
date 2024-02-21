using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class MatchingWordsQuestion : Question
    {
        public List<WordPair>? WordPairs { get; private set; }

        private MatchingWordsQuestion(string questionRequirement, LearningType questionLearningType, Guid lessonId) : base(questionRequirement, questionLearningType, lessonId)
        {
            QuestionId = Guid.NewGuid();
            QuestionRequirement = questionRequirement;
            QuestionLearningType = questionLearningType;
            LessonId = lessonId;
        }

        public static Result<MatchingWordsQuestion> Create(string questionRequirement, LearningType questionLearningType, Guid lessonId)
        {
            if (string.IsNullOrWhiteSpace(questionRequirement))
                return Result<MatchingWordsQuestion>.Failure("QuestionRequirement is required");

            if (!IsValidLearningType(questionLearningType))
                return Result<MatchingWordsQuestion>.Failure("Invalid LearningType");

            if (lessonId == default)
                return Result<MatchingWordsQuestion>.Failure("LessonId is required");


            return Result<MatchingWordsQuestion>.Success(new MatchingWordsQuestion(questionRequirement, questionLearningType, lessonId));
        }

        public void AttachWordPair(WordPair wordPair)
        {
            if (WordPairs != null)
                WordPairs.Add(wordPair);
        }

        public void RemoveWordPair(WordPair wordPair)
        {
            if (WordPairs != null)
                WordPairs.Remove(wordPair);
        }

        public bool IsWordPairExist(WordPair wordPair)
        {
            if (WordPairs != null)
                return WordPairs.Contains(wordPair);
            return false;
        }
    }
}

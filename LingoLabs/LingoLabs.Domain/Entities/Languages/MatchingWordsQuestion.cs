using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class MatchingWordsQuestion : Question
    {
        public Dictionary<string, string>? WordPairs { get; private set; } = new();
        
        private MatchingWordsQuestion(string questionRequirement, LearningType questionLearningType) : base(questionRequirement, questionLearningType)
        {
        }

        public static Result<MatchingWordsQuestion> Create(string questionRequirement, LearningType questionLearningType)
        {
            if (string.IsNullOrWhiteSpace(questionRequirement))
                return Result<MatchingWordsQuestion>.Failure("QuestionRequirement is required");

            if (!IsValidLearningType(questionLearningType))
                return Result<MatchingWordsQuestion>.Failure("Invalid LearningType");

            return Result<MatchingWordsQuestion>.Success(new MatchingWordsQuestion(questionRequirement, questionLearningType));
        }

        public void AttachWordPairs(string word, string translation)
        {
            if (WordPairs == null)
                WordPairs = new Dictionary<string, string> { { word, translation } };
            else
                WordPairs.Add(word, translation);
        }

        public void RemoveWordPair(string word)
        {
            if (WordPairs != null)
                WordPairs.Remove(word);
        }

        public bool IsWordPairCorrect(string word, string translation)
        {
            if (WordPairs != null)
                return WordPairs.ContainsKey(word) && WordPairs[word] == translation;
            return false;
        }
    }
}

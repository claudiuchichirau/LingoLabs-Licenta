using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class MatchingWordsQuestion : Question
    {
        public List<WordPair> WordPairs { get; private set; }

        private MatchingWordsQuestion(string questionRequirement, LearningType questionLearningType, List<WordPair> wordPairs) : base(questionRequirement, questionLearningType)
        {
            if (wordPairs != null || wordPairs.Count > 0)
                WordPairs = wordPairs;
        }

        public static Result<MatchingWordsQuestion> Create(string questionRequirement, LearningType questionLearningType, List<WordPair> wordPairs)
        {
            if (string.IsNullOrWhiteSpace(questionRequirement))
                return Result<MatchingWordsQuestion>.Failure("QuestionRequirement is required");

            if (!IsValidLearningType(questionLearningType))
                return Result<MatchingWordsQuestion>.Failure("Invalid LearningType");

            if (wordPairs == null || wordPairs.Count == 0)
                return Result<MatchingWordsQuestion>.Failure("WordPairs is required");

            return Result<MatchingWordsQuestion>.Success(new MatchingWordsQuestion(questionRequirement, questionLearningType, wordPairs));
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

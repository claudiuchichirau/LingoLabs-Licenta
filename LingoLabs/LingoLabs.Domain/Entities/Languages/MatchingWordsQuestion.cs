using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class MatchingWordsQuestion : Question
    {
        public ICollection<WordPair>? WordPairs { get; private set; } = new List<WordPair>();

        private MatchingWordsQuestion(string questionRequirement, LearningType questionLearningType) : base(questionRequirement, questionLearningType)
        {
        }

        public static Result<MatchingWordsQuestion> Create(string questionRequirement, LearningType questionLearningType)
        {
            if (string.IsNullOrWhiteSpace(questionRequirement))
                return Result<MatchingWordsQuestion>.Failure("QuestionRequirement is required");

            return Result<MatchingWordsQuestion>.Success(new MatchingWordsQuestion(questionRequirement, questionLearningType));
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

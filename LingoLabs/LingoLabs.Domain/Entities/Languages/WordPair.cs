using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class WordPair: AuditableEntity
    {
        public Guid WordPairId { get; private set; }
        public string KeyWord { get; private set; } = string.Empty;
        public string ValueWord { get; private set; } = string.Empty;
        public Guid MatchingWordsQuestionId { get; private set; }
        public MatchingWordsQuestion? MatchingWordsQuestion { get; private set; }

        private WordPair(string keyWord, string valueWord, Guid matchingWordsQuestionId)
        {
            WordPairId = Guid.NewGuid();
            KeyWord = keyWord;
            ValueWord = valueWord;
            MatchingWordsQuestionId = matchingWordsQuestionId;
        }

        public static Result<WordPair> Create(string keyWord, string valueWord, Guid matchingWordsQuestionId)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
                return Result<WordPair>.Failure("KeyWord is required");

            if (string.IsNullOrWhiteSpace(valueWord))
                return Result<WordPair>.Failure("ValueWord is required");

            if (matchingWordsQuestionId == default)
                return Result<WordPair>.Failure("MatchingWordsQuestionId is required");

            return Result<WordPair>.Success(new WordPair(keyWord, valueWord, matchingWordsQuestionId));
        }

        public void Update(string keyWord, string valueWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
                KeyWord = keyWord;
            if (string.IsNullOrWhiteSpace(valueWord))
                ValueWord = valueWord;
        }
    }
}
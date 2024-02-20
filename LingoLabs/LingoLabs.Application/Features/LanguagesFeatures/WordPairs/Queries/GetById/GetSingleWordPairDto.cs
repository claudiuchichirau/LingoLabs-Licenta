namespace LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Queries.GetById
{
    public class GetSingleWordPairDto: WordPairDto
    {
        public Guid MatchingWordsQuestionId { get; set; }
    }
}

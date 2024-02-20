namespace LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Commands.CreateWordPair
{
    public class CreateWordPairDto
    {
        public Guid WordPairId { get; set; }
        public string? KeyWord { get; set; }
        public string? ValueWord { get; set; }
        public Guid? MatchingWordsQuestionId { get; set; }
    }
}

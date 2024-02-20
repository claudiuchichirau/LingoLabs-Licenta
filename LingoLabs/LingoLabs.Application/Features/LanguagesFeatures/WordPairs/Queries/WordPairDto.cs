namespace LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Queries
{
    public class WordPairDto
    {
        public Guid WordPairId { get; set; }
        public string KeyWord { get; set; } = default!;
        public string ValueWord { get; set; } = default!;
    }
}

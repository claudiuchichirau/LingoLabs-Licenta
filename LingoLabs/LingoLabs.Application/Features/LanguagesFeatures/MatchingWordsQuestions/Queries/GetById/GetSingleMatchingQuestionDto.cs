using LingoLabs.Application.Features.LanguagesFeatures.Choices.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Queries.GetById
{
    public class GetSingleMatchingQuestionDto: MatchingWordsQuestionDto
    {
        public List<WordPairDto> WordPairs { get; set; } = [];
        public byte[] QuestionImageData { get; set; } = [];
        public string QuestionVideoLink { get; set; } = string.Empty;
    }
}

using LingoLabs.Application.Features.LanguagesFeatures.Choices.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Queries.GetById
{
    public class GetSingleMatchingQuestionDto: MatchingWordsQuestionDto
    {
        public List<ChoiceDto> QuestionChoices { get; set; } = [];
        public byte[] QuestionImageData { get; set; } = [];
        public string QuestionVideoLink { get; set; } = string.Empty;
    }
}

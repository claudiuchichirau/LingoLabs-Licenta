using LingoLabs.Application.Features.LanguagesFeatures.Choices.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetById
{
    public class GetSingleQuestionDto: QuestionDto
    {
        public List<ChoiceDto> QuestionChoices { get; set; } = [];
        public string QuestionImageData { get; set; } = string.Empty;
        public string QuestionVideoLink { get; set; } = string.Empty;
        public Guid? LanguageId { get; set; }
    }
}

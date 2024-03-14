using LingoLabs.Application.Features.LanguagesFeatures.Choices.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetById
{
    public class GetSingleQuestionDto: QuestionDto
    {
        public List<ChoiceDto> QuestionChoices { get; set; } = [];
        public byte[] QuestionImageData { get; set; } = [];
        public string QuestionVideoLink { get; set; } = string.Empty;
        public int? QuestionPriorityNumber { get; set; }
        public Guid? LanguageId { get; set; }
    }
}

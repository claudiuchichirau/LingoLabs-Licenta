using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Commands.CreateMatchingWordsQuestion
{
    public class CreateMatchingWordsQuestionCommandResponse: BaseResponse
    {
        public CreateMatchingWordsQuestionCommandResponse() : base()
        {
        }
        
        public CreateMatchingWordsQuestionDto MatchingWordsQuestion { get; set; }
    }
}

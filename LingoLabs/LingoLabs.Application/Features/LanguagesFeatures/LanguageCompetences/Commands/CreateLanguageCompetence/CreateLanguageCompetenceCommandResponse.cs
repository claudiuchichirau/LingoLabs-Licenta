using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.CreateLanguageCompetence
{
    public class CreateLanguageCompetenceCommandResponse: BaseResponse
    {
        public CreateLanguageCompetenceCommandResponse() : base()
        {
        }

        public CreateLanguageCompetenceDto LanguageCompetence { get; set; }
    }
}

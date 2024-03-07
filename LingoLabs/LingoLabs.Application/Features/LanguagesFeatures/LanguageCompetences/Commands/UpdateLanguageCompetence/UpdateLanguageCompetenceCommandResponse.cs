using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.UpdateLanguageCompetence
{
    public class UpdateLanguageCompetenceCommandResponse: BaseResponse
    {
        public UpdateLanguageCompetenceDto? UpdateLanguageCompetenceDto { get; set; }
    }
}

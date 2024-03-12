using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandResponse: BaseResponse
    {
        public UpdateLanguageDto? UpdateLanguage { get; set; }
    }
}

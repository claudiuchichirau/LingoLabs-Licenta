using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand: IRequest<UpdateLanguageCommandResponse>
    {
        public Guid LanguageId { get; set; }
        public string LanguageName { get; set; } = string.Empty;
        public string LanguageDescription { get; set; } = string.Empty;
        public string LanguageVideoLink { get; set; } = string.Empty;
        public string LanguageFlag { get; set; } = string.Empty;
        //public UpdateLanguageDto UpdateLanguageDto { get; set; } = new UpdateLanguageDto();
    }
}

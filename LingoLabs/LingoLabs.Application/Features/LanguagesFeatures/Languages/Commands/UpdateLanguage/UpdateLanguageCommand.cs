using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand: IRequest<UpdateLanguageCommandResponse>
    {
        public Guid LanguageId { get; set; }
        public UpdateLanguageDto UpdateLanguageDto { get; set; } = new UpdateLanguageDto();
    }
}

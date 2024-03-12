using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommand: IRequest<DeleteLanguageCommandResponse>
    {
        public Guid LanguageId { get; set; }
    }
}

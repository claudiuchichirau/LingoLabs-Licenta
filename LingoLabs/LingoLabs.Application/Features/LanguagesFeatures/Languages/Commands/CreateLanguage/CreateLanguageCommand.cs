using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommand : IRequest<CreateLanguageCommandResponse>
    {
        public string LanguageName { get; set; } = default!;
    }
}

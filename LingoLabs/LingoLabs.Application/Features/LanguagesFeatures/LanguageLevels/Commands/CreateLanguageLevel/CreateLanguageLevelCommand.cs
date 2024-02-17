using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.CreateLanguageLevel
{
    public class CreateLanguageLevelCommand: IRequest<CreateLanguageLevelCommandResponse>
    {
        public string LanguageLevelName { get; set; }
        public string LanguageLevelAlias { get; set; }
    }
}

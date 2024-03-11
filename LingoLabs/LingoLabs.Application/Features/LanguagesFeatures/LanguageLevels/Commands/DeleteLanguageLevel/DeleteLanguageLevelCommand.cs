using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.DeleteLanguageLevel
{
    public class DeleteLanguageLevelCommand: IRequest<DeleteLanguageLevelCommandResponse>
    {
        public Guid LanguageLevelId { get; set; }
    }
}

using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.UpdateLanguageLevel
{
    public class UpdateLanguageLevelCommand: IRequest<UpdateLanguageLevelCommandResponse>
    {
        public Guid LanguageLevelId { get; set; }
        public UpdateLanguageLevelDto UpdateLanguageLevelDto { get; set; } = new UpdateLanguageLevelDto();
    }
}

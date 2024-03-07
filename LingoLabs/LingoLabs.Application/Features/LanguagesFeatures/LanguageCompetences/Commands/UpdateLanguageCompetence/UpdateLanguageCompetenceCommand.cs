using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.UpdateLanguageCompetence
{
    public class UpdateLanguageCompetenceCommand: IRequest<UpdateLanguageCompetenceCommandResponse>
    {
        public Guid LanguageCompetenceId { get; set; }
        public UpdateLanguageCompetenceDto UpdateLanguageCompetenceDto { get; set; } = new UpdateLanguageCompetenceDto();
    }
}

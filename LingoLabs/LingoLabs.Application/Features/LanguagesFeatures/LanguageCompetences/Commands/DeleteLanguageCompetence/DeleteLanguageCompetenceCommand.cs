using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.DeleteLanguageCompetence
{
    public class DeleteLanguageCompetenceCommand: IRequest<DeleteLanguageCompetenceCommandResponse>
    {
        public Guid LanguageCompetenceId { get; set; }
    }
}

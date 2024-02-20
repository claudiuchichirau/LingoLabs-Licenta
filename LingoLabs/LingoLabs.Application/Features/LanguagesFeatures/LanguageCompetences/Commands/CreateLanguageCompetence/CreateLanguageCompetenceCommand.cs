using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.CreateLanguageCompetence
{
    public class CreateLanguageCompetenceCommand: IRequest<CreateLanguageCompetenceCommandResponse>
    {
        public string LanguageCompetenceName { get; set; } = default!;
        public LanguageCompetenceType LanguageCompetenceType { get; set; }
        public Guid ChapterId { get; set; }
        public Guid LanguageId { get; set; }
    }
}

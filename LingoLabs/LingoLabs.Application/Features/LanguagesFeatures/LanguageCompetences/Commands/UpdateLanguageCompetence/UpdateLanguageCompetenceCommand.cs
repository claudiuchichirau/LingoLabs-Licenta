using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.UpdateLanguageCompetence
{
    public class UpdateLanguageCompetenceCommand: IRequest<UpdateLanguageCompetenceCommandResponse>
    {
        public Guid LanguageCompetenceId { get; set; }
        //public string LanguageCompetenceName { get; set; } = string.Empty;
        public string LanguageCompetenceDescription { get; set; } = string.Empty;
        public string LanguageCompetenceVideoLink { get; set; } = string.Empty;
        public int? LanguageCompetencePriorityNumber { get; set; }
    }
}

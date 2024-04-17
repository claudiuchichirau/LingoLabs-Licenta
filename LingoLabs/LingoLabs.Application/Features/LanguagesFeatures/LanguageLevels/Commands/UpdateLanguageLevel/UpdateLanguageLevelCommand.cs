using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.UpdateLanguageLevel
{
    public class UpdateLanguageLevelCommand: IRequest<UpdateLanguageLevelCommandResponse>
    {
        public Guid LanguageLevelId { get; set; }
        public string LanguageLevelName { get; set; } = string.Empty;
        public string LanguageLevelAlias { get; set; } = string.Empty;
        public string LanguageLevelDescription { get; set; } = string.Empty;
        public string LanguageLevelVideoLink { get; set; } = string.Empty;
        public int? LanguageLevelPriorityNumber { get; set; }
    }
}

using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.UpdateLanguageCompetence;
using LingoLabs.Application.Persistence.Languages;
using MediatR;
using System.Web;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.UpdateLanguageLevel
{
    public class UpdateLanguageLevelCommandHandler : IRequestHandler<UpdateLanguageLevelCommand, UpdateLanguageLevelCommandResponse>
    {
        private readonly ILanguageLevelRepository languageLevelRepository;
        private readonly ILanguageRepository languageRepository;

        public UpdateLanguageLevelCommandHandler(ILanguageLevelRepository languageLevelRepository, ILanguageRepository languageRepository)
        {
            this.languageLevelRepository = languageLevelRepository;
            this.languageRepository = languageRepository;
        }
        public async Task<UpdateLanguageLevelCommandResponse> Handle(UpdateLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLanguageLevelCommandValidator(languageLevelRepository, languageRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid) 
            {
                return new UpdateLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var languageLevel = await languageLevelRepository.FindByIdAsync(request.LanguageLevelId);

            if(!languageLevel.IsSuccess)
            {
                return new UpdateLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { languageLevel.Error }
                };
            }

            string newVideoLink = null;

            if (!string.IsNullOrEmpty(request.LanguageLevelVideoLink))
            {
                string videoId = HttpUtility.ParseQueryString(new Uri(request.LanguageLevelVideoLink).Query).Get("v");

                // Construct the new URL
                newVideoLink = $"https://www.youtube.com/embed/{videoId}";
            }

            int? prNumb = request.LanguageLevelPriorityNumber;

            languageLevel.Value.UpdateLanguageLevel(
                request.LanguageLevelName,
                request.LanguageLevelAlias,
                request.LanguageLevelDescription,
                newVideoLink,
                request.LanguageLevelPriorityNumber);

            await languageLevelRepository.UpdateAsync(languageLevel.Value);

            return new UpdateLanguageLevelCommandResponse
            {
                Success = true,
                UpdateLanguageLevel = new UpdateLanguageLevelDto
                {
                    LanguageLevelAlias = languageLevel.Value.LanguageLevelAlias,
                    LanguageLevelDescription = languageLevel.Value.LanguageLevelDescription,
                    LanguageLevelVideoLink = languageLevel.Value.LanguageLevelVideoLink,
                    PriorityNumber = languageLevel.Value.PriorityNumber
                }
            };
        }
    }
}

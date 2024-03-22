using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, UpdateLanguageCommandResponse>
    {
        private readonly ILanguageRepository languageRepository;

        public UpdateLanguageCommandHandler(ILanguageRepository languageRepository)
        {
            this.languageRepository = languageRepository;
        }
        public async Task<UpdateLanguageCommandResponse> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLanguageCommandValidator(languageRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid) 
            {
                return new UpdateLanguageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var language = await languageRepository.FindByIdAsync(request.LanguageId);

            if(!language.IsSuccess)
            {
                return new UpdateLanguageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { language.Error }
                };
            }

            var updateLanguageDto = request.UpdateLanguageDto;

            language.Value.UpdateLanguage(updateLanguageDto.LanguageName, updateLanguageDto.LanguageDescription, updateLanguageDto.LanguageVideoLink, updateLanguageDto.LanguageFlag);

            var result = await languageRepository.UpdateAsync(language.Value);

            if(!result.IsSuccess)
            {
                return new UpdateLanguageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }

            return new UpdateLanguageCommandResponse
            {
                Success = true
            };

        }
    }
}

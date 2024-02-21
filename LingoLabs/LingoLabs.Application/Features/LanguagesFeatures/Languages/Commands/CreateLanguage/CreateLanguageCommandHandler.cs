using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;


namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, CreateLanguageCommandResponse>
    {
        private readonly ILanguageRepository repository;

        public CreateLanguageCommandHandler(ILanguageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateLanguageCommandResponse> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLanguageCommandValidator(repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateLanguageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var language = Language.Create(request.LanguageName);
            if (language.IsSuccess)
            {
                await repository.AddAsync(language.Value);
                return new CreateLanguageCommandResponse
                {
                    Language = new CreateLanguageDto
                    {
                        LanguageId = language.Value.LanguageId,
                        LanguageName = language.Value.LanguageName
                    }
                };
            }
            return new CreateLanguageCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { language.Error }
            };
        }
    }
}

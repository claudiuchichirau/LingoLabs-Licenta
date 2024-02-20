using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.CreateLanguageLevel
{
    public class CreateLanguageLevelCommandHandler: IRequestHandler<CreateLanguageLevelCommand, CreateLanguageLevelCommandResponse>
    {
        private readonly ILanguageLevelRepository repository;

        public CreateLanguageLevelCommandHandler(ILanguageLevelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateLanguageLevelCommandResponse> Handle(CreateLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLanguageLevelCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new CreateLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var languageLevel = LanguageLevel.Create(request.LanguageLevelName, request.LanguageLevelAlias, request.LanguageId);
            if(languageLevel.IsSuccess)
            {
                await repository.AddAsync(languageLevel.Value);
                return new CreateLanguageLevelCommandResponse
                {
                    LanguageLevel = new CreateLanguageLevelDto
                    {
                        LanguageLevelId = languageLevel.Value.LanguageLevelId,
                        LanguageLevelName = languageLevel.Value.LanguageLevelName,
                        LanguageLevelAlias = languageLevel.Value.LanguageLevelAlias,
                        LanguageId = languageLevel.Value.LanguageId
                    }
                };
            }

            return new CreateLanguageLevelCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { languageLevel.Error }
            };
        }
    }
}

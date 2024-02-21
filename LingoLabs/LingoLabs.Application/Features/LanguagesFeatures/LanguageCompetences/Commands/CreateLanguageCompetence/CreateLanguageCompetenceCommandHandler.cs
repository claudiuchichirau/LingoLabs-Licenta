using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.CreateLanguageCompetence
{
    public class CreateLanguageCompetenceCommandHandler: IRequestHandler<CreateLanguageCompetenceCommand, CreateLanguageCompetenceCommandResponse>
    {
        private readonly ILanguageCompetenceRepository repository;

        public CreateLanguageCompetenceCommandHandler(ILanguageCompetenceRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateLanguageCompetenceCommandResponse> Handle(CreateLanguageCompetenceCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLanguageCompetenceCommandValidator(repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new CreateLanguageCompetenceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var languageCompetence = LanguageCompetence.Create(request.LanguageCompetenceName, request.LanguageCompetenceType, request.ChapterId, request.LanguageId);
            if(languageCompetence.IsSuccess)
            {
                await repository.AddAsync(languageCompetence.Value);
                return new CreateLanguageCompetenceCommandResponse
                {
                    LanguageCompetence = new CreateLanguageCompetenceDto
                    {
                        LanguageCompetenceId = languageCompetence.Value.LanguageCompetenceId,
                        LanguageCompetenceName = languageCompetence.Value.LanguageCompetenceName,
                        LanguageCompetenceType = languageCompetence.Value.LanguageCompetenceType,
                        ChapterId = languageCompetence.Value.ChapterId,
                        LanguageId = languageCompetence.Value.LanguageId
                    }
                };
            }

            return new CreateLanguageCompetenceCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { languageCompetence.Error }
            };
        }
    }
}

using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.CreateLanguageCompetenceResult
{
    public class CreateLanguageCompetenceResultCommandHandler: IRequestHandler<CreateLanguageCompetenceResultCommand, CreateLanguageCompetenceResultCommandResponse>
    {
        private readonly ILanguageCompetenceResultRepository repository;

        public CreateLanguageCompetenceResultCommandHandler(ILanguageCompetenceResultRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateLanguageCompetenceResultCommandResponse> Handle(CreateLanguageCompetenceResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLanguageCompResultCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new CreateLanguageCompetenceResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var languageCompetenceResult = LanguageCompetenceResult.Create(request.LanguageCompetenceId, request.EnrollmentId, request.IsCompleted);
            if(languageCompetenceResult.IsSuccess)
            {
                await repository.AddAsync(languageCompetenceResult.Value);
                return new CreateLanguageCompetenceResultCommandResponse
                {
                    LanguageCompetenceResult = new CreateLanguageCompetenceResultDto
                    {
                        LanguageCompetenceResultId = languageCompetenceResult.Value.LanguageCompetenceResultId,
                        LanguageCompetenceId = languageCompetenceResult.Value.LanguageCompetenceId,
                        EnrollmentId = languageCompetenceResult.Value.EnrollmentId,
                        IsCompleted = false
                    }
                };
            }

            return new CreateLanguageCompetenceResultCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { languageCompetenceResult.Error }
            };
        }
    }
}

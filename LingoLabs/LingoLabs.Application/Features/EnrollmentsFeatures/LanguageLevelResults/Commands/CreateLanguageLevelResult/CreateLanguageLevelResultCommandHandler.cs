using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.CreateLanguageLevelResult
{
    public class CreateLanguageLevelResultCommandHandler: IRequestHandler<CreateLanguageLevelResultCommand, CreateLanguageLevelResultCommandResponse>
    {
        private readonly ILanguageLevelResultRepository repository;

        public CreateLanguageLevelResultCommandHandler(ILanguageLevelResultRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateLanguageLevelResultCommandResponse> Handle(CreateLanguageLevelResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLanguageLevelResultCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if(!validationResult.IsValid)
            {
                return new CreateLanguageLevelResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var languageLevelResult = LanguageLevelResult.Create(request.LanguageLevelId, request.EnrollmentId);
            if(languageLevelResult.IsSuccess)
            {
                await repository.AddAsync(languageLevelResult.Value);
                return new CreateLanguageLevelResultCommandResponse
                {
                    LanguageLevelResult = new CreateLanguageLevelResultDto
                    {
                        LanguageLevelResultId = languageLevelResult.Value.LanguageLevelResultId,
                        LanguageLevelId = languageLevelResult.Value.LanguageLevelId,
                        EnrollmentId = languageLevelResult.Value.EnrollmentId,
                        IsCompleted = false 
                    }
                };
            }

            return new CreateLanguageLevelResultCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { languageLevelResult.Error }
            };
        }
    }
}

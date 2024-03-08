using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.UpdateLanguageCompetenceResult
{
    public class UpdateLanguageCompetenceResultCommandHandler : IRequestHandler<UpdateLanguageCompetenceResultCommand, UpdateLanguageCompetenceResultCommandResponse>
    {
        private readonly ILanguageCompetenceResultRepository languageCompetenceResultRepository;

        public UpdateLanguageCompetenceResultCommandHandler(ILanguageCompetenceResultRepository languageCompetenceResultRepository)
        {
            this.languageCompetenceResultRepository = languageCompetenceResultRepository;
        }
        public async Task<UpdateLanguageCompetenceResultCommandResponse> Handle(UpdateLanguageCompetenceResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLanguageCompetenceResultComandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new UpdateLanguageCompetenceResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var languageCompetenceResult = await languageCompetenceResultRepository.FindByIdAsync(request.LanguageCompetenceResultId);

            if(!languageCompetenceResult.IsSuccess)
            {
                return new UpdateLanguageCompetenceResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { languageCompetenceResult.Error }
                };
            }

            var updateLanguageCompetenceResultDto = request.UpdateLanguageCompetenceResultDto;

            languageCompetenceResult.Value.UpdateLanguageCompetenceResult(updateLanguageCompetenceResultDto.IsCompleted);

            await languageCompetenceResultRepository.UpdateAsync(languageCompetenceResult.Value);

            return new UpdateLanguageCompetenceResultCommandResponse
            {
                Success = true,
                UpdateLanguageCompetenceResult = new UpdateLanguageCompetenceResultDto
                {
                    IsCompleted = updateLanguageCompetenceResultDto.IsCompleted
                }
            };
        }
    }
}

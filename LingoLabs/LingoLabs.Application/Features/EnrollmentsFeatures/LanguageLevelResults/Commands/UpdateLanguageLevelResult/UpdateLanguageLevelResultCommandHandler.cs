using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.UpdateLanguageLevelResult
{
    public class UpdateLanguageLevelResultCommandHandler : IRequestHandler<UpdateLanguageLevelResultCommand, UpdateLanguageLevelResultCommandResponse>
    {
        private readonly ILanguageLevelResultRepository languageLevelResultRepository;

        public UpdateLanguageLevelResultCommandHandler(ILanguageLevelResultRepository languageLevelResultRepository)
        {
            this.languageLevelResultRepository = languageLevelResultRepository;
        }
        public async Task<UpdateLanguageLevelResultCommandResponse> Handle(UpdateLanguageLevelResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLanguageLevelResultCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new UpdateLanguageLevelResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var languageLevelResult = await languageLevelResultRepository.FindByIdAsync(request.LanguageLevelResultId);

            if(!languageLevelResult.IsSuccess)
            {
                return new UpdateLanguageLevelResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { languageLevelResult.Error }
                };
            }

            var updateLanguageLevelResultDto = request.UpdateLanguageLevelResultDto;

            languageLevelResult.Value.UpdateLanguageLevelResult(updateLanguageLevelResultDto.IsCompleted);

            await languageLevelResultRepository.UpdateAsync(languageLevelResult.Value);

            return new UpdateLanguageLevelResultCommandResponse
            {
                Success = true,
                UpdateLanguageLevelResultDto = new UpdateLanguageLevelResultDto
                {
                    IsCompleted = languageLevelResult.Value.IsCompleted
                }
            };
        }
    }
}

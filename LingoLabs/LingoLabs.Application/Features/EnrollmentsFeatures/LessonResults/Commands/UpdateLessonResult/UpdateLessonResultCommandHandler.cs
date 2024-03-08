using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.UpdateLessonResult
{
    public class UpdateLessonResultCommandHandler : IRequestHandler<UpdateLessonResultCommand, UpdateLessonResultCommandResponse>
    {
        private readonly ILessonResultRepository lessonResultRepository;

        public UpdateLessonResultCommandHandler(ILessonResultRepository lessonResultRepository)
        {
            this.lessonResultRepository = lessonResultRepository;
        }
        public async Task<UpdateLessonResultCommandResponse> Handle(UpdateLessonResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLessonResultCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new UpdateLessonResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var lessonResult = await lessonResultRepository.FindByIdAsync(request.LessonResultId);

            if(!lessonResult.IsSuccess)
            {
                return new UpdateLessonResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { lessonResult.Error }
                };
            }

            var updateLessonResultDto = request.UpdateLessonResultDto;

            lessonResult.Value.UpdateLessonResult(updateLessonResultDto.IsCompleted);

            await lessonResultRepository.UpdateAsync(lessonResult.Value);

            return new UpdateLessonResultCommandResponse
            {
                Success = true,
                UpdateLessonResult = new UpdateLessonResultDto
                {
                    IsCompleted = lessonResult.Value.IsCompleted
                }
            };
        }
    }
}

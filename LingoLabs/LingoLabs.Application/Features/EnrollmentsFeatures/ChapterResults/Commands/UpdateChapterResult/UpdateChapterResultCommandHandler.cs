using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.UpdateChapterResult
{
    public class UpdateChapterResultCommandHandler : IRequestHandler<UpdateChapterResultCommand, UpdateChapterResultCommandResponse>
    {
        private readonly IChapterResultRepository chapterResultRepository;

        public UpdateChapterResultCommandHandler(IChapterResultRepository chapterResultRepository)
        {
            this.chapterResultRepository = chapterResultRepository;
        }
        public async Task<UpdateChapterResultCommandResponse> Handle(UpdateChapterResultCommand request, CancellationToken cancellationToken)
        {
            var chapterResult = await chapterResultRepository.FindByIdAsync(request.ChapterResultId);

            if(!chapterResult.IsSuccess)
            {
                return new UpdateChapterResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapterResult.Error }
                };
            }

            var updateChapterResultDto = request.UpdateChapterResultDto;

            chapterResult.Value.UpdateChapterResult(updateChapterResultDto.IsCompleted);

            await chapterResultRepository.UpdateAsync(chapterResult.Value);

            return new UpdateChapterResultCommandResponse
            {
                Success = true,
                UpdateChapterResult = new UpdateChapterResultDto
                {
                    IsCompleted = chapterResult.Value.IsCompleted
                }
            };
        }
    }
}

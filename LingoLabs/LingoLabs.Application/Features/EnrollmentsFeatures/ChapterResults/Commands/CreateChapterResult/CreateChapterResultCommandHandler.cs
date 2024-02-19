using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.CreateChapterResult
{
    public class CreateChapterResultCommandHandler : IRequestHandler<CreateChapterResultCommand, CreateChapterResultCommandResponse>
    {
        private readonly IChapterResultRepository repository;

        public CreateChapterResultCommandHandler(IChapterResultRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateChapterResultCommandResponse> Handle(CreateChapterResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateChapterResultCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateChapterResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var chapterResult = ChapterResult.Create(request.ChapterId, request.LanguageLevelResultId);

            if (chapterResult.IsSuccess)
            {
                await repository.AddAsync(chapterResult.Value);
                return new CreateChapterResultCommandResponse
                {
                    ChapterResult = new CreateChapterResultDto
                    {
                        ChapterResultId = chapterResult.Value.ChapterResultId,
                        ChapterId = chapterResult.Value.ChapterId,
                        LanguageLevelResultId = chapterResult.Value.LanguageLevelResultId
                    }
                };
            }

            return new CreateChapterResultCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { chapterResult.Error }
            };
        }
    }
}

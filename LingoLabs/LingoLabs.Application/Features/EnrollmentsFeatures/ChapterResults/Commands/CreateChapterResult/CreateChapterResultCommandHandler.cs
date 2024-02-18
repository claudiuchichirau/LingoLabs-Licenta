using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.CreateChapterResult
{
    public class CreateChapterResultCommandHandler : IRequestHandler<CreateChapterResultCommand, CreateChapterResultCommandResponse>
    {
        private readonly IChapterRepository repository;

        public CreateChapterResultCommandHandler(IChapterRepository repository)
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

            var chapterResult = ChapterResult.Create(request.ChapterId, request.LanguageCompetenceResults);

        }
    }
}

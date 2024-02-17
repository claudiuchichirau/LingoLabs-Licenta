using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommandHandler : IRequestHandler<CreateChapterCommand, CreateChapterCommandResponse>
    {
        private readonly IChapterRepository repository;

        public CreateChapterCommandHandler(IChapterRepository repository)
        {
            this.repository = repository;
        }
        
        public async Task<CreateChapterCommandResponse> Handle(CreateChapterCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateChapterCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var chapter = Chapter.Create(request.ChapterName);
            if (chapter.IsSuccess)
            {
                await repository.AddAsync(chapter.Value);
                return new CreateChapterCommandResponse
                {
                    Chapter = new CreateChapterDto
                    {
                        ChapterId = chapter.Value.ChapterId,
                        ChapterName = chapter.Value.ChapterName
                    }
                };
            }
            return new CreateChapterCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { chapter.Error }
            };
        }
    }
}

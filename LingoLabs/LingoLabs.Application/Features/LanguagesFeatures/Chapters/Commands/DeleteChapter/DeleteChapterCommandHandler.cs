using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommandHandler : IRequestHandler<DeleteChapterCommand, DeleteChapterCommandResponse>
    {
        private readonly IChapterRepository chapterRepository;

        public DeleteChapterCommandHandler(IChapterRepository chapterRepository)
        {
            this.chapterRepository = chapterRepository;
        }
        public async Task<DeleteChapterCommandResponse> Handle(DeleteChapterCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteChapterCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new DeleteChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var chapter = await chapterRepository.FindByIdAsync(request.ChapterId);
            if(!chapter.IsSuccess)
            {
                return new DeleteChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapter.Error }
                };
            }

            await chapterRepository.DeleteAsync(request.ChapterId);

            return new DeleteChapterCommandResponse
            {
                Success = true
            };
        }
    }
}

using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterCommandHandler : IRequestHandler<UpdateChapterCommandCommand, UpdateChapterCommandResponse>
    {
        private readonly IChapterRepository chapterRepository;

        public UpdateChapterCommandHandler(IChapterRepository chapterRepository)
        {
            this.chapterRepository = chapterRepository;
        }
        public async Task<UpdateChapterCommandResponse> Handle(UpdateChapterCommandCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateChapterCommandValidator(chapterRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new UpdateChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var chapter = await chapterRepository.FindByIdAsync(request.ChapterId);

            if(!chapter.IsSuccess)
            {
                return new UpdateChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapter.Error }
                };
            }

            var updateChapterDto = request.UpdateChapterDto;

            chapter.Value.UpdateChapter(
                updateChapterDto.ChapterName,
                updateChapterDto.ChapterDescription,
                updateChapterDto.ChapterImageData,
                updateChapterDto.ChapterVideoLink,
                updateChapterDto.ChapterPriorityNumber
            );

            await chapterRepository.UpdateAsync(chapter.Value);

            return new UpdateChapterCommandResponse
            {
                Success = true,
                UpdateChapter = new UpdateChapterDto
                {
                    ChapterName = chapter.Value.ChapterName,
                    ChapterDescription = chapter.Value.ChapterDescription,
                    ChapterPriorityNumber = chapter.Value.ChapterPriorityNumber,
                    ChapterImageData = chapter.Value.ChapterImageData,
                    ChapterVideoLink = chapter.Value.ChapterVideoLink
                }
            };
        }
    }
}

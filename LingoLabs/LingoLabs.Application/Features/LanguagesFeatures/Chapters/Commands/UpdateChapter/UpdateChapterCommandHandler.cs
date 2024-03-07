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
                updateChapterDto.ChapterNumber,
                updateChapterDto.ChapterImageData,
                updateChapterDto.ChapterVideoLink
            );

            await chapterRepository.UpdateAsync(chapter.Value);

            return new UpdateChapterCommandResponse
            {
                Success = true,
                UpdateChapter = new UpdateChapterDto
                {
                    ChapterName = chapter.Value.ChapterName,
                    ChapterDescription = chapter.Value.ChapterDescription,
                    ChapterNumber = chapter.Value.ChapterNumber ?? 0,
                    ChapterImageData = chapter.Value.ChapterImageData,
                    ChapterVideoLink = chapter.Value.ChapterVideoLink
                }
            };
        }
    }
}

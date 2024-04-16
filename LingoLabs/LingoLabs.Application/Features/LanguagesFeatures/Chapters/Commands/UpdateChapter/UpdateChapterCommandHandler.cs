using LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdateLanguage;
using LingoLabs.Application.Persistence.Languages;
using MediatR;
using System.Web;

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

            var videoId = HttpUtility.ParseQueryString(new Uri(updateChapterDto.ChapterVideoLink).Query).Get("v");

            // Construct the new URL
            var newVideoLink = $"https://www.youtube.com/embed/{videoId}";

            chapter.Value.UpdateChapter(
                updateChapterDto.ChapterName,
                updateChapterDto.ChapterDescription,
                updateChapterDto.ChapterImageData,
                newVideoLink,
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

using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterCommandResponse: BaseResponse
    {
        public UpdateChapterDto? UpdateChapter { get; set; }
    }
}

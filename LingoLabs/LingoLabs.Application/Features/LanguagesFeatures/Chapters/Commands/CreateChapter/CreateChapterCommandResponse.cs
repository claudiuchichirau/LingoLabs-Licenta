using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommandResponse : BaseResponse
    {
        public CreateChapterCommandResponse() : base()
        {
        }

        public CreateChapterDto Chapter { get; set; } = default!;
    }
}

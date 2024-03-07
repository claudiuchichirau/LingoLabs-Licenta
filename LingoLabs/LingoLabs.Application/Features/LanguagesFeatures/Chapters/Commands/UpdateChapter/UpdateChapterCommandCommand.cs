using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterCommandCommand: IRequest<UpdateChapterCommandResponse>
    {
        public Guid ChapterId { get; set; }
        public UpdateChapterDto UpdateChapterDto { get; set; } = new UpdateChapterDto();
    }
}

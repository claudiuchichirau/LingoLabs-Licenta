using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommand: IRequest<DeleteChapterCommandResponse>
    {
        public Guid ChapterId { get; set; }
    }
}

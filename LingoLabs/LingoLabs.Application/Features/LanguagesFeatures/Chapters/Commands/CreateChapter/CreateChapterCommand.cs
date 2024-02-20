using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommand : IRequest<CreateChapterCommandResponse>
    {
        public string ChapterName { get; set; } = default!;
        public Guid LanguageLevelId { get; set; }
    }
}

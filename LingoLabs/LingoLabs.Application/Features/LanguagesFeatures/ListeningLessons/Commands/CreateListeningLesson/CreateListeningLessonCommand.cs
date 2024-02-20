using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.CreateListeningLesson
{
    public class CreateListeningLessonCommand: IRequest<CreateListeningLessonCommandResponse>
    {
        public string LessonTitle { get; set; } = default!;
        public LanguageCompetenceType LessonType { get; set; }
        public Guid LanguageCompetenceId { get; set; }
        public List<byte[]> AudioContents { get; set; } = default!;
        public List<string> Accents { get; set; } = default!;
    }
}

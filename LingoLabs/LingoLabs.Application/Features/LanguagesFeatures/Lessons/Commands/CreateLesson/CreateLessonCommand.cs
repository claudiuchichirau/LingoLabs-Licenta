using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommand: IRequest<CreateLessonCommandResponse>
    {
        public string LessonTitle { get; private set; }
        public LanguageCompetenceType LessonType { get; private set; }
    }
}

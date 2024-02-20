using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Queries.GetById
{
    public class GetByIdListeningLessonQueryHandler : IRequestHandler<GetByIdListeningLessonQuery, GetSingleListeningLessonDto>
    {
        private readonly IListeningLessonRepository repository;

        public GetByIdListeningLessonQueryHandler(IListeningLessonRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetSingleListeningLessonDto> Handle(GetByIdListeningLessonQuery request, CancellationToken cancellationToken)
        {
            var listeningLesson = await repository.FindByIdAsync(request.Id);
            if(listeningLesson.IsSuccess)
            {
                return new GetSingleListeningLessonDto
                {
                    LessonId = listeningLesson.Value.LessonId,
                    LessonTitle = listeningLesson.Value.LessonTitle,
                    LessonDescription = listeningLesson.Value.LessonDescription,
                    LessonRequirement = listeningLesson.Value.LessonRequirement,
                    LessonContent = listeningLesson.Value.LessonContent,
                    LessonType = listeningLesson.Value.LessonType,
                    LessonVideoLink = listeningLesson.Value.LessonVideoLink,
                    LessonImageData = listeningLesson.Value.LessonImageData,
                    LanguageCompetenceId = listeningLesson.Value.LanguageCompetenceId,
                    LessonQuestions = listeningLesson.Value.LessonQuestions.Select(c => new Questions.Queries.QuestionDto
                    {
                        QuestionId = c.QuestionId,
                        QuestionRequirement = c.QuestionRequirement,
                        QuestionLearningType = c.QuestionLearningType,
                        LessonId = c.LessonId
                    }).ToList(),
                    AudioContents = listeningLesson.Value.AudioContents,
                    Accents = listeningLesson.Value.Accents
                };
            }

            return new GetSingleListeningLessonDto();
        }
    }
}

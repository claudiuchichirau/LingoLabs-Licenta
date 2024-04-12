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
                var sortedLessonQuestions = listeningLesson.Value.LessonQuestions
                    .OrderBy(question => question.QuestionPriorityNumber ?? int.MaxValue)
                    .Select(question => new Questions.Queries.QuestionDto
                    {
                        QuestionId = question.QuestionId,
                        QuestionRequirement = question.QuestionRequirement,
                        QuestionType = question.QuestionType,
                        LessonId = question.LessonId
                    }).ToList();

                return new GetSingleListeningLessonDto
                {
                    LessonId = listeningLesson.Value.LessonId,
                    LessonTitle = listeningLesson.Value.LessonTitle,
                    LessonDescription = listeningLesson.Value.LessonDescription,
                    LessonRequirement = listeningLesson.Value.LessonRequirement,
                    LessonContent = listeningLesson.Value.LessonContent,
                    LessonVideoLink = listeningLesson.Value.LessonVideoLink,
                    LessonImageData = listeningLesson.Value.LessonImageData,
                    LessonPriorityNumber = listeningLesson.Value.LessonPriorityNumber,
                    ChapterId = listeningLesson.Value.ChapterId,
                    LanguageCompetenceId = listeningLesson.Value.LanguageCompetenceId,
                    LessonQuestions = sortedLessonQuestions,
                    TextScript = listeningLesson.Value.TextScript,
                    Accents = listeningLesson.Value.Accents
                };
            }

            return new GetSingleListeningLessonDto();
        }
    }
}

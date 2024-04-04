using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Queries.GetAll
{
    public class GetAllListeningLessonsQueryHandler : IRequestHandler<GetAllListeningLessonsQuery, GetAllListeningLessonsResponse>
    {
        private readonly IListeningLessonRepository repository;

        public GetAllListeningLessonsQueryHandler(IListeningLessonRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetAllListeningLessonsResponse> Handle(GetAllListeningLessonsQuery request, CancellationToken cancellationToken)
        {
            GetAllListeningLessonsResponse response = new();
            var result = await repository.GetAllAsync();
            if(result.IsSuccess)
            {
                response.ListeningLessons = result.Value.Select(lesson => new ListeningLessonDto
                {
                    LessonId = lesson.LessonId,
                    LessonTitle = lesson.LessonTitle,
                    ChapterId = lesson.ChapterId,
                    LanguageCompetenceId = lesson.LanguageCompetenceId,
                    TextScript = lesson.TextScript,
                    Accents = lesson.Accents
                }).ToList();
            }
            return response;
        }
    }
}

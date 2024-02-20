using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries.GetAll
{
    public class GetAllLessonsQueryHandler : IRequestHandler<GetAllLessonsQuery, GetAllLessonsResponse>
    {
        private readonly ILessonRepository repository;

        public GetAllLessonsQueryHandler(ILessonRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetAllLessonsResponse> Handle(GetAllLessonsQuery request, CancellationToken cancellationToken)
        {
            GetAllLessonsResponse response = new();
            var result = await repository.GetAllAsync();
            if(result.IsSuccess)
            {
                response.Lessons = result.Value.Select(lesson => new LessonDto
                {
                    LessonId = lesson.LessonId,
                    LessonTitle = lesson.LessonTitle,
                    LessonType = lesson.LessonType,
                    LanguageCompetenceId = lesson.LanguageCompetenceId
                }).ToList();
            }

            return response;
        }
    }
}

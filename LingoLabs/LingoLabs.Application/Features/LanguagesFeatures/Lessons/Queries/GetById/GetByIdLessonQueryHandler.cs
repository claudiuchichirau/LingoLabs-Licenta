using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries.GetById
{
    public class GetByIdLessonQueryHandler : IRequestHandler<GetByIdLessonQuery, GetSingleLessonDto>
    {
        private readonly ILessonRepository repository;

        public GetByIdLessonQueryHandler(ILessonRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetSingleLessonDto> Handle(GetByIdLessonQuery request, CancellationToken cancellationToken)
        {
            var lesson = await repository.FindByIdAsync(request.Id);
            if(lesson.IsSuccess)
            {
                return new GetSingleLessonDto
                {
                    LessonId = lesson.Value.LessonId,
                    LessonTitle = lesson.Value.LessonTitle,
                    LessonDescription = lesson.Value.LessonDescription,
                    LessonRequirement = lesson.Value.LessonRequirement,
                    LessonContent = lesson.Value.LessonContent,
                    LessonType = lesson.Value.LessonType,
                    LessonVideoLink = lesson.Value.LessonVideoLink,
                    LessonImageData = lesson.Value.LessonImageData,
                    LanguageCompetenceId = lesson.Value.LanguageCompetenceId,
                    LessonQuestions = lesson.Value.LessonQuestions.Select(c => new Questions.Queries.QuestionDto
                    {
                        QuestionId = c.QuestionId,
                        QuestionRequirement = c.QuestionRequirement,
                        QuestionLearningType = c.QuestionLearningType,
                        LessonId = c.LessonId
                    }).ToList()
                };
            }
            
            return new GetSingleLessonDto();
        }
    }
}

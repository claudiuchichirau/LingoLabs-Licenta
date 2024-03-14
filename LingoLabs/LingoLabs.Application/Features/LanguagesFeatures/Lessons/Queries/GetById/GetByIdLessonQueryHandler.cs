using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
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
                var sortedLessonQuestions = lesson.Value.LessonQuestions
                    .OrderBy(question => question.QuestionPriorityNumber ?? int.MaxValue)
                    .Select(question => new Questions.Queries.QuestionDto
                    {
                        QuestionId = question.QuestionId,
                        QuestionRequirement = question.QuestionRequirement,
                        QuestionLearningType = question.QuestionLearningType,
                        LessonId = question.LessonId
                    }).ToList();

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
                    LessonPriorityNumber = lesson.Value.LessonPriorityNumber,
                    LessonQuestions = sortedLessonQuestions
                };
            }
            
            return new GetSingleLessonDto();
        }
    }
}

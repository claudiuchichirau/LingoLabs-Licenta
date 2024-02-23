using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Queries.GetById
{
    public class GetByIdLessonResultQueryHandler : IRequestHandler<GetByIdLessonResultQuery, GetSingleLessonResultDto>
    {
        private readonly ILessonResultRepository lessonResultRepository;

        public GetByIdLessonResultQueryHandler(ILessonResultRepository lessonResultRepository)
        {
            this.lessonResultRepository = lessonResultRepository;
        }
        public async Task<GetSingleLessonResultDto> Handle(GetByIdLessonResultQuery request, CancellationToken cancellationToken)
        {
            var lessonResult = await lessonResultRepository.FindByIdAsync(request.Id);
            if(lessonResult.IsSuccess)
            {
                return new GetSingleLessonResultDto
                {
                    LessonResultId = lessonResult.Value.LessonResultId,
                    LessonId = lessonResult.Value.LessonId,
                    LanguageCompetenceResultId = lessonResult.Value.LanguageCompetenceResultId,
                    IsCompleted = lessonResult.Value.IsCompleted ?? false,
                    QuestionResults = lessonResult.Value.QuestionResults?.Select(q => new QuestionResults.Queries.QuestionResultDto
                    {
                        QuestionResultId = q.QuestionResultId,
                        QuestionId = q.QuestionId,
                        LessonResultId = q.LessonResultId,
                        IsCorrect = q.IsCorrect,
                    }).ToList()
                };
            }

            return new GetSingleLessonResultDto();
        }
    }
}

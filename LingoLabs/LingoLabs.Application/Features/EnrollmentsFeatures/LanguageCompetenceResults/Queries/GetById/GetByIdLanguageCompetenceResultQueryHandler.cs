using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Queries;
using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Queries.GetById
{
    public class GetByIdLanguageCompetenceResultQueryHandler : IRequestHandler<GetByIdLanguageCompetenceResultQuery, GetSingleLanguageCompetenceResultDto>
    {
        private readonly ILanguageCompetenceResultRepository languageCompetenceResultRepository;

        public GetByIdLanguageCompetenceResultQueryHandler(ILanguageCompetenceResultRepository languageCompetenceResultRepository)
        {
            this.languageCompetenceResultRepository = languageCompetenceResultRepository;
        }

        public async Task<GetSingleLanguageCompetenceResultDto> Handle(GetByIdLanguageCompetenceResultQuery request, CancellationToken cancellationToken)
        {
            var languageCompetenceResult = await languageCompetenceResultRepository.FindByIdAsync(request.Id);
            if (languageCompetenceResult.IsSuccess)
            {
                return new GetSingleLanguageCompetenceResultDto
                {
                    LanguageCompetenceResultId = languageCompetenceResult.Value.LanguageCompetenceResultId,
                    LanguageCompetenceId = languageCompetenceResult.Value.LanguageCompetenceId,
                    EnrollmentId = languageCompetenceResult.Value.EnrollmentId,
                    IsCompleted = languageCompetenceResult.Value.IsCompleted,
                    LessonsResults = languageCompetenceResult.Value.LessonsResults.Select(x => new LessonResultDto
                    {
                        LessonResultId = x.LessonResultId,
                        LessonId = x.LessonId,
                        ChapterResultId = x.ChapterResultId,
                        LanguageCompetenceResultId = x.LanguageCompetenceResultId,
                        IsCompleted = x.IsCompleted,
                    }).ToList()
                };
            }

            return new GetSingleLanguageCompetenceResultDto();
        }
    }
}

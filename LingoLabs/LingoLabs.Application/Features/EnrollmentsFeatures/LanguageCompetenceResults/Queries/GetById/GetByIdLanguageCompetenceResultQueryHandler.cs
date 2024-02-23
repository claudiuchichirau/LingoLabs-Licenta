using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Queries.GetById
{
    public class GetByIdLanguageCompetenceResultQueryHandler: IRequestHandler<GetByIdLanguageCompetenceResultQuery, GetSingleLanguageCompetenceResultDto>
    {
        private readonly ILanguageCompetenceResultRepository languageCompetenceResultRepository;

        public GetByIdLanguageCompetenceResultQueryHandler(ILanguageCompetenceResultRepository languageCompetenceResultRepository)
        {
            this.languageCompetenceResultRepository = languageCompetenceResultRepository;
        }

        public async Task<GetSingleLanguageCompetenceResultDto> Handle(GetByIdLanguageCompetenceResultQuery request, CancellationToken cancellationToken)
        {
            var languageCompetenceResult = await languageCompetenceResultRepository.FindByIdAsync(request.Id);
            if(languageCompetenceResult.IsSuccess)
            {
                return new GetSingleLanguageCompetenceResultDto
                {
                    LanguageCompetenceResultId = languageCompetenceResult.Value.LanguageCompetenceResultId,
                    LanguageCompetenceId = languageCompetenceResult.Value.LanguageCompetenceId,
                    ChapterResultId = languageCompetenceResult.Value.ChapterResultId,
                    IsCompleted = languageCompetenceResult.Value.IsCompleted ?? false,
                    LessonsResults = languageCompetenceResult.Value.LessonsResults.Select(l => new LessonResults.Queries.LessonResultDto
                    {
                        LessonResultId = l.LessonResultId,
                        LessonId = l.LessonId,
                        LanguageCompetenceResultId = l.LanguageCompetenceResultId,
                        IsCompleted = l.IsCompleted ?? false,
                    }).ToList()
                };
            }

            return new GetSingleLanguageCompetenceResultDto();
        }
    }
}

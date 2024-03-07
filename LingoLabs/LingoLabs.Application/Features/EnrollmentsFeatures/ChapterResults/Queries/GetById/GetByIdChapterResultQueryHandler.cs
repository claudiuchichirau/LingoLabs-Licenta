using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Queries.GetById
{
    public class GetByIdChapterResultQueryHandler : IRequestHandler<GetByIdChapterResultQuery, GetSingleChapterResultDto>
    {
        private readonly IChapterResultRepository chapterResultRepository;

        public GetByIdChapterResultQueryHandler(IChapterResultRepository chapterResultRepository)
        {
            this.chapterResultRepository = chapterResultRepository;
        }
        public async Task<GetSingleChapterResultDto> Handle(GetByIdChapterResultQuery request, CancellationToken cancellationToken)
        {
            var chapterResult = await chapterResultRepository.FindByIdAsync(request.Id);
            if(chapterResult.IsSuccess)
            {
                return new GetSingleChapterResultDto
                {
                    ChapterResultId = chapterResult.Value.ChapterResultId,
                    ChapterId = chapterResult.Value.ChapterId,
                    LanguageLevelResultId = chapterResult.Value.LanguageLevelResultId,
                    IsCompleted = chapterResult.Value.IsCompleted,
                    LanguageCompetenceResults = chapterResult.Value.LanguageCompetenceResults.Select(lc => new LanguageCompetenceResults.Queries.LanguageCompetenceResultDto
                    {
                        LanguageCompetenceResultId = lc.LanguageCompetenceResultId,
                        LanguageCompetenceId = lc.LanguageCompetenceId,
                        ChapterResultId = lc.ChapterResultId,
                        IsCompleted = lc.IsCompleted,
                    }).ToList()

                };
            }

            return new GetSingleChapterResultDto();
        }
    }
}

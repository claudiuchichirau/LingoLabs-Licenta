using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Queries.GetById
{
    public class GetByIdLanguageLevelResultQueryHandler : IRequestHandler<GetByIdLanguageLevelResultQuery, GetSingleLanguageLevelResultDto>
    {
        private readonly ILanguageLevelResultRepository languageLevelResultRepository;

        public GetByIdLanguageLevelResultQueryHandler(ILanguageLevelResultRepository languageLevelResultRepository)
        {
            this.languageLevelResultRepository = languageLevelResultRepository;
        }
        public async Task<GetSingleLanguageLevelResultDto> Handle(GetByIdLanguageLevelResultQuery request, CancellationToken cancellationToken)
        {
            var languageLevelResult = await languageLevelResultRepository.FindByIdAsync(request.Id);
            if(languageLevelResult.IsSuccess)
            {
                return new GetSingleLanguageLevelResultDto
                {
                    LanguageLevelResultId = languageLevelResult.Value.LanguageLevelResultId,
                    LanguageLevelId = languageLevelResult.Value.LanguageLevelId,
                    EnrollmentId = languageLevelResult.Value.EnrollmentId,
                    IsCompleted = languageLevelResult.Value.IsCompleted,
                    ChapterResults = languageLevelResult.Value.ChapterResults.Select(chapterResult => new ChapterResults.Queries.ChapterResultDto
                    {
                        ChapterResultId = chapterResult.ChapterResultId,
                        LanguageLevelResultId = chapterResult.LanguageLevelResultId,
                        ChapterId = chapterResult.ChapterId,
                        IsCompleted = chapterResult.IsCompleted
                    }).ToList()
                };
            }

            return new GetSingleLanguageLevelResultDto();
        }
    }
}

using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries.GetAll
{
    public class GetAllChaptersQueryHandler : IRequestHandler<GetAllChaptersQuery, GetAllChaptersResponse>
    {
        private readonly IChapterRepository repository;

        public GetAllChaptersQueryHandler(IChapterRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetAllChaptersResponse> Handle(GetAllChaptersQuery request, CancellationToken cancellationToken)
        {
            GetAllChaptersResponse response = new();
            var result = await repository.GetAllAsync();
            if(result.IsSuccess)
            {
                response.Chapters = result.Value.Select(c => new ChapterDto
                {
                    ChapterId = c.ChapterId,
                    ChapterName = c.ChapterName,
                    LanguageLevelId = c.LanguageLevelId,
                    LanguageCompetenceId = c.LanguageCompetenceId
                }).ToList();
            }

            return response;
        }
    }
}

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
                    ChapterPriorityNumber = c.ChapterPriorityNumber,
                    ChapterName = c.ChapterName,
                    ChapterDescription = c.ChapterDescription,
                    ChapterVideoLink = c.ChapterVideoLink,
                    ChapterImageData = c.ChapterImageData,
                    ChapterLessons = c.ChapterLessons.Select(lesson => new ListeningLessons.Queries.ListeningLessonDto
                    {
                        LessonId = lesson.LessonId,
                        LessonPriorityNumber = lesson.LessonPriorityNumber,
                        LessonTitle = lesson.LessonTitle,
                        LessonDescription = lesson.LessonDescription,
                        LessonContent = lesson.LessonContent,
                        ChapterId = lesson.ChapterId,
                        LanguageCompetenceId = lesson.LanguageCompetenceId
                    }).ToList(),

                    LanguageLevelId = c.LanguageLevelId
                }).ToList();
            }

            return response;
        }
    }
}

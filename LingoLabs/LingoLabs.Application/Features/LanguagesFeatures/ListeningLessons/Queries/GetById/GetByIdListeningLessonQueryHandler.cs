using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Queries.GetById
{
    public class GetByIdListeningLessonQueryHandler : IRequestHandler<GetByIdListeningLessonQuery, GetSingleListeningLessonDto>
    {
        private readonly IListeningLessonRepository listeningLessonRepository;
        private readonly IChapterRepository chapterRepository;
        private readonly ILanguageCompetenceRepository languageCompetenceRepository;
        private readonly ILanguageLevelRepository languageLevelRepository;

        public GetByIdListeningLessonQueryHandler(IListeningLessonRepository listeningLessonRepository, IChapterRepository chapterRepository, ILanguageCompetenceRepository languageCompetenceRepository, ILanguageLevelRepository languageLevelRepository)
        {
            this.listeningLessonRepository = listeningLessonRepository;
            this.chapterRepository = chapterRepository;
            this.languageCompetenceRepository = languageCompetenceRepository;
            this.languageLevelRepository = languageLevelRepository;
        }
        public async Task<GetSingleListeningLessonDto> Handle(GetByIdListeningLessonQuery request, CancellationToken cancellationToken)
        {
            var listeningLesson = await listeningLessonRepository.FindByIdAsync(request.Id);
            if(listeningLesson.IsSuccess)
            {
                var sortedLessonQuestions = listeningLesson.Value.LessonQuestions
                    .OrderBy(question => question.QuestionPriorityNumber ?? int.MaxValue)
                    .Select(question => new Questions.Queries.QuestionDto
                    {
                        QuestionId = question.QuestionId,
                        QuestionRequirement = question.QuestionRequirement,
                        QuestionType = question.QuestionType,
                        LessonId = question.LessonId
                    }).ToList();

                var chapter = await chapterRepository.FindByIdAsync(listeningLesson.Value.ChapterId);

                var languageCompetence = await languageCompetenceRepository.FindByIdAsync(listeningLesson.Value.LanguageCompetenceId);

                var languageLevel = await languageLevelRepository.FindByIdAsync(chapter.Value.LanguageLevelId);

                return new GetSingleListeningLessonDto
                {
                    LessonId = listeningLesson.Value.LessonId,
                    LessonTitle = listeningLesson.Value.LessonTitle,
                    LessonDescription = listeningLesson.Value.LessonDescription,
                    LessonRequirement = listeningLesson.Value.LessonRequirement,
                    LessonContent = listeningLesson.Value.LessonContent,
                    LessonVideoLink = listeningLesson.Value.LessonVideoLink,
                    LessonImageData = listeningLesson.Value.LessonImageData,
                    LessonPriorityNumber = listeningLesson.Value.LessonPriorityNumber,
                    ChapterId = listeningLesson.Value.ChapterId,
                    LanguageCompetenceId = listeningLesson.Value.LanguageCompetenceId,
                    ChapterName = chapter.Value.ChapterName,
                    LanguageCompetenceName = languageCompetence.Value.LanguageCompetenceName,
                    LanguageLevelName = languageLevel.Value.LanguageLevelName,
                    LessonQuestions = sortedLessonQuestions,
                    TextScript = listeningLesson.Value.TextScript,
                    Accents = listeningLesson.Value.Accents
                };
            }

            return new GetSingleListeningLessonDto();
        }
    }
}

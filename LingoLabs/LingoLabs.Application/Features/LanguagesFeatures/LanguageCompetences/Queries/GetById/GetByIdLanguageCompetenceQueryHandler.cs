using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries.GetById
{
    public class GetByIdLanguageCompetenceQueryHandler : IRequestHandler<GetByIdLanguageCompetenceQuery, GetSingleLanguageCompetenceDto>
    {
        private readonly ILanguageCompetenceRepository repository;

        public GetByIdLanguageCompetenceQueryHandler(ILanguageCompetenceRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetSingleLanguageCompetenceDto> Handle(GetByIdLanguageCompetenceQuery request, CancellationToken cancellationToken)
        {
            var languageCompetence = await repository.FindByIdAsync(request.Id);
            if(languageCompetence.IsSuccess)
            {
                return new GetSingleLanguageCompetenceDto
                {
                    LanguageCompetenceId = languageCompetence.Value.LanguageCompetenceId,
                    LanguageCompetenceName = languageCompetence.Value.LanguageCompetenceName,
                    LanguageCompetenceType = languageCompetence.Value.LanguageCompetenceType,
                    ChapterId = languageCompetence.Value.ChapterId,
                    LanguageId = languageCompetence.Value.LanguageId,
                    LanguageCompetenceDescription = languageCompetence.Value.LanguageCompetenceDescription,
                    LanguageCompetenceVideoLink = languageCompetence.Value.LanguageCompetenceVideoLink,

                    Lessons = languageCompetence.Value.Lessons.Select(lesson => new Lessons.Queries.LessonDto
                    {
                        LessonId = lesson.LessonId,
                        LessonTitle = lesson.LessonTitle,
                        LessonType = lesson.LessonType,
                        LanguageCompetenceId = lesson.LanguageCompetenceId
                    }).ToList(),

                    LearningCompetenceKeyWords = languageCompetence.Value.LearningCompetenceKeyWords.Select(tag => new Tags.Queries.TagDto
                    {
                        TagId = tag.TagId,
                        TagContent = tag.TagContent
                    }).ToList()
                };
            }

            return new GetSingleLanguageCompetenceDto();
        }
    }
}

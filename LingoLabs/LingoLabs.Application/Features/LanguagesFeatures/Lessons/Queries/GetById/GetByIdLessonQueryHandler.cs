using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries.GetById
{
    public class GetByIdLessonQueryHandler : IRequestHandler<GetByIdLessonQuery, GetSingleLessonDto>
    {
        private readonly ILessonRepository repository;
        private readonly ITagRepository tagRepository;

        public GetByIdLessonQueryHandler(ILessonRepository repository, ITagRepository tagRepository)
        {
            this.repository = repository;
            this.tagRepository = tagRepository;
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
                        QuestionType = question.QuestionType,
                        LessonId = question.LessonId
                    }).ToList();

                var allTags = await tagRepository.GetAllAsync();

                var allTagsDto = allTags.Value.Select(tag => new Tags.Queries.TagDto
                {
                    TagId = tag.TagId,
                    TagContent = tag.TagContent
                });

                var tasks = lesson.Value.LessonTags.Select(async entityTag =>
                {
                    var tag = await tagRepository.FindByIdAsync(entityTag.TagId);
                    return new Tags.Queries.TagDto
                    {
                        TagId = entityTag.Tag.TagId,
                        TagContent = tag.Value.TagContent
                    };
                });
                var lessonKeyWords = (await Task.WhenAll(tasks)).ToList();

                var unassociatedTags = allTagsDto.Where(tag => !lessonKeyWords.Any(lkw => lkw.TagId == tag.TagId)).ToList();

                return new GetSingleLessonDto
                {
                    LessonId = lesson.Value.LessonId,
                    LessonTitle = lesson.Value.LessonTitle,
                    LessonDescription = lesson.Value.LessonDescription,
                    LessonRequirement = lesson.Value.LessonRequirement,
                    LessonContent = lesson.Value.LessonContent,
                    LessonVideoLink = lesson.Value.LessonVideoLink,
                    LessonImageData = lesson.Value.LessonImageData,
                    ChapterId = lesson.Value.ChapterId,
                    LanguageCompetenceId = lesson.Value.LanguageCompetenceId,
                    LessonPriorityNumber = lesson.Value.LessonPriorityNumber,
                    LessonQuestions = sortedLessonQuestions,
                    LessonTags = lessonKeyWords,
                    UnassociatedTags = unassociatedTags
                };
            }
            
            return new GetSingleLessonDto();
        }
    }
}

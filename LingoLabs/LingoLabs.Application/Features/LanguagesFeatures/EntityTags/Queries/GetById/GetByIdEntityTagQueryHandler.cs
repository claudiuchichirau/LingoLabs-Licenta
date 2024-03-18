using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Queries.GetById
{
    public class GetByIdEntityTagQueryHandler : IRequestHandler<GetByIdEntityTagQuery, EntityTagDto>
    {
        private readonly IEntityTagRepository entityTagRepository;

        public GetByIdEntityTagQueryHandler(IEntityTagRepository entityTagRepository)
        {
            this.entityTagRepository = entityTagRepository;
        }

        public async Task<EntityTagDto> Handle(GetByIdEntityTagQuery request, CancellationToken cancellationToken)
        {
            var entityTag = await entityTagRepository.FindByIdAsync(request.Id);
            if (entityTag.IsSuccess)
            {
                return new EntityTagDto
                {
                    EntityTagId = entityTag.Value.EntityTagId,
                    LanguageId = entityTag.Value.LanguageId,
                    LanguageLevelId = entityTag.Value.LanguageLevelId,
                    ChapterId = entityTag.Value.ChapterId,
                    LanguageCompetenceId = entityTag.Value.LanguageCompetenceId,
                    LessonId = entityTag.Value.LessonId,
                    TagId = entityTag.Value.TagId
                };
            }

            return new EntityTagDto();
        }
    }
}

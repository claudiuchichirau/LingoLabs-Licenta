using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Queries.GetAll
{
    public class GetAllEntityTagsQueryHandler: IRequestHandler<GetAllEntityTagsQuery, GetAllEntityTagsResponse>
    {
        private readonly IEntityTagRepository entityTagRepository;

        public GetAllEntityTagsQueryHandler(IEntityTagRepository entityTagRepository)
        {
            this.entityTagRepository = entityTagRepository;
        }

        public async Task<GetAllEntityTagsResponse> Handle(GetAllEntityTagsQuery request, CancellationToken cancellationToken)
        {
            GetAllEntityTagsResponse response = new();
            var result = await entityTagRepository.GetAllAsync();
            if(result.IsSuccess)
            {
                response.EntityTags = result.Value.Select(e => new EntityTagDto
                {
                    EntityTagId = e.EntityTagId,
                    LanguageId = e.LanguageId,
                    LanguageCompetenceId = e.LanguageCompetenceId,
                    LanguageLevelId = e.LanguageLevelId,
                    LessonId = e.LessonId,
                    TagId = e.TagId
                }).ToList();
            }

            return response;
        }
    }
}

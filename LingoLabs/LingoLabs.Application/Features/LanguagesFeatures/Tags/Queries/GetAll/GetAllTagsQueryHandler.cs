using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries.GetAll
{
    public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, GetAllTagsResponse>
    {
        private readonly ITagRepository repository;

        public GetAllTagsQueryHandler(ITagRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetAllTagsResponse> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            GetAllTagsResponse response = new();
            var result = await repository.GetAllAsync();
            if(result.IsSuccess)
            {
                response.Tags = result.Value.Select(tag => new TagDto
                {
                    TagId = tag.TagId,
                    TagContent = tag.TagContent
                }).ToList();
            }

            return response;
        }
    }
}

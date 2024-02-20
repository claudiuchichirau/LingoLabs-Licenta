using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries.GetById
{
    public class GetByIdTagQueryHandler : IRequestHandler<GetByIdTagQuery, TagDto>
    {
        private readonly ITagRepository repository;

        public GetByIdTagQueryHandler(ITagRepository repository)
        {
            this.repository = repository;
        }
        public async Task<TagDto> Handle(GetByIdTagQuery request, CancellationToken cancellationToken)
        {
            var tag = await repository.FindByIdAsync(request.Id);
            if(tag.IsSuccess)
            {
                return new TagDto
                {
                    TagId = tag.Value.TagId,
                    TagContent = tag.Value.TagContent
                };
            }

            return new TagDto();
        }
    }
}

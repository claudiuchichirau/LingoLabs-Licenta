using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries.GetById
{
    public record class GetByIdTagQuery(Guid Id): IRequest<TagDto>;
}

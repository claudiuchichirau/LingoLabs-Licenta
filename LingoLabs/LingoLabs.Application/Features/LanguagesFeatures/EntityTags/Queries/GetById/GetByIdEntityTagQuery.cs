using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Queries.GetById
{
    public record GetByIdEntityTagQuery(Guid Id): IRequest<EntityTagDto>;
}

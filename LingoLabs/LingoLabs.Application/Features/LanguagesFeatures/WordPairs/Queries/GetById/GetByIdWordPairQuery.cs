using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Queries.GetById
{
    public record class GetByIdWordPairQuery(Guid Id): IRequest<GetSingleWordPairDto>;
}

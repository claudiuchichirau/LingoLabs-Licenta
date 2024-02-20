using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries.GetById
{
    public record class GetByIdLanguageLevelQuery(Guid Id): IRequest<LanguageLevelDto>;
}

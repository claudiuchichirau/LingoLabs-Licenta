using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Queries.GetById
{
    public record class GetByIdUserLanguageLevelQuery(Guid Id): IRequest<UserLanguageLevelDto>;
}

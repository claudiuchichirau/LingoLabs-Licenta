using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Queries.GetById
{
    public record class GetByIdLanguageLevelResultQuery(Guid Id): IRequest<GetSingleLanguageLevelResultDto>;
}

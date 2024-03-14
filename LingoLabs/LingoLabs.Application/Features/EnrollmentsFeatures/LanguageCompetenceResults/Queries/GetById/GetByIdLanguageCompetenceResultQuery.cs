using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Queries.GetById
{
    public record class GetByIdLanguageCompetenceResultQuery(Guid Id) : IRequest<GetSingleLanguageCompetenceResultDto>;
}

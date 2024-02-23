using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ReadingQuestionResults.Queries.GetById
{
    public record class GetByIdReadingQuestionResultQuery(Guid Id): IRequest<ReadingQuestionResultDto>;
}

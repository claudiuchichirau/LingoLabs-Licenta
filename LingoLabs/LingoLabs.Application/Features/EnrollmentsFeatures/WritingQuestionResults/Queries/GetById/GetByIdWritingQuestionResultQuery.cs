using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.WritingQuestionResults.Queries.GetById
{
    public record class GetByIdWritingQuestionResultQuery(Guid Id): IRequest<WritingQuestionResultDto>;
}

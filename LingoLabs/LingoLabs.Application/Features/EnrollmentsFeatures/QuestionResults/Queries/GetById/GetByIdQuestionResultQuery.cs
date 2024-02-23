using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Queries.GetById
{
    public record class GetByIdQuestionResultQuery(Guid Id): IRequest<QuestionResultDto>;
}

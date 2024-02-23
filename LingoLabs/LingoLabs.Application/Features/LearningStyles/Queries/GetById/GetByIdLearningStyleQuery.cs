using MediatR;

namespace LingoLabs.Application.Features.LearningStyles.Queries.GetById
{
    public record class GetByIdLearningStyleQuery(Guid Id): IRequest<GetSingleLearningStyleDto>;
}

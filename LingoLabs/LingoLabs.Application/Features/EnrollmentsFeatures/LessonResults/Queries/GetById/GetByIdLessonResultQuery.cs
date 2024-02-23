using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Queries.GetById
{
    public record class GetByIdLessonResultQuery(Guid Id): IRequest<GetSingleLessonResultDto>;
}

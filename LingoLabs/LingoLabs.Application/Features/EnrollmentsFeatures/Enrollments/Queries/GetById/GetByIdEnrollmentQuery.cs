using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Queries.GetById
{
    public record class GetByIdEnrollmentQuery(Guid Id): IRequest<GetSingleEnrollmentDto>;
}

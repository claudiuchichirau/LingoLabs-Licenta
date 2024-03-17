using LingoLabs.Application.Contracts.Interfaces;
using LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Queries.GetAll;
using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Queries.GetAllByUserId
{
    public class GetAllEnrollmentsByUserIdQueryHandler : IRequestHandler<GetAllEnrollmentsByUserIdQuery, GetAllEnrollmentsByUserIdResponse>
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly ICurrentUserService currentUserService;

        public GetAllEnrollmentsByUserIdQueryHandler(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService)
        {
            this.enrollmentRepository = enrollmentRepository;
            this.currentUserService = currentUserService;
        }
        public async Task<GetAllEnrollmentsByUserIdResponse> Handle(GetAllEnrollmentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(currentUserService.UserId);

            GetAllEnrollmentsByUserIdResponse response = new();

            var result = await enrollmentRepository.GetEnrollmentsByUserIdAsync(userId);

            if(result.IsSuccess)
            {
                response.Enrollments = result.Value.Select(x => new EnrollmentDto
                {
                    EnrollmentId = x.EnrollmentId,
                    UserId = x.UserId,
                    LanguageId = x.LanguageId
                }).ToList();
            }

            return response;
        }
    }
}

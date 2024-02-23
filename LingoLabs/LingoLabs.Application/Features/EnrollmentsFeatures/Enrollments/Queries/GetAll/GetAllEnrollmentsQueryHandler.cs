using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Queries.GetAll
{
    public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetAllEnrollmentsQuery, GetAllEnrollmentsResponse>
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetAllEnrollmentsQueryHandler(IEnrollmentRepository enrollmentRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
        }
        public async Task<GetAllEnrollmentsResponse> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            GetAllEnrollmentsResponse response = new();
            var result = await enrollmentRepository.GetAllAsync();
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

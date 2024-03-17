using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;

namespace LingoLabs.Application.Persistence.Enrollments
{
    public interface IEnrollmentRepository: IAsyncRepository<Enrollment>
    {
        Task<Result<List<Enrollment>>> GetEnrollmentsByUserIdAsync(Guid userId);
    }
}

using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class EnrollmentRepository: BaseRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

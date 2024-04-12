using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class EnrollmentRepository: BaseRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<Enrollment>> FindByIdAsync(Guid guid)
        {
            var enrollment = await context.Enrollments
                .Include(e => e.UserLanguageLevels)
                .Include(e => e.LanguageLevelResults)
                .Include(e => e.LanguageCompetenceResults)
                .FirstOrDefaultAsync(e => e.EnrollmentId == guid);

            if(enrollment == null)
                return Result<Enrollment>.Failure($"Enrollment with id {guid} not found");

            return Result<Enrollment>.Success(enrollment);
        }

        public async Task<Result<List<Enrollment>>> GetEnrollmentsByUserIdAsync(Guid userId)
        {
            var enrollments = await context.Enrollments
                .Include(e => e.UserLanguageLevels)
                .Include(e => e.LanguageLevelResults)
                .Where(e => e.UserId == userId)
                .ToListAsync();

            if(enrollments == null)
                return Result<List<Enrollment>>.Failure($"Enrollments for user with id {userId} not found");

            return Result<List<Enrollment>>.Success(enrollments);
        }
    }
}

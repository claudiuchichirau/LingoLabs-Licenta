using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class UserLanguageLevelRepository: BaseRepository<UserLanguageLevel>, IUserLanguageLevelRepository
    {
        public UserLanguageLevelRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<UserLanguageLevel>> FindByIdAsync(Guid id)
        {
            var userLanguageLevel = await context.UserLanguageLevels
                .Include(ull => ull.LanguageLevel)
                .Include(ull => ull.Enrollment)
                .FirstOrDefaultAsync(ull => ull.UserLanguageLevelId == id);

            if(userLanguageLevel == null)
                return Result<UserLanguageLevel>.Failure($"UserLanguageLevel with id {id} not found");

            return Result<UserLanguageLevel>.Success(userLanguageLevel);
        }

        //public async Task<Result<UserLanguageLevel>> GetUserLanguageLevelByEnrollmentId(Guid enrollmentId)
        //{
        //    var userLanguageLevel = await context.UserLanguageLevels
        //        .Where(userLanguageLevel => userLanguageLevel.EnrollmentId == enrollmentId)
        //        .Include(ull => ull.LanguageLevel)
        //        .FirstOrDefaultAsync(ull => ull.EnrollmentId == enrollmentId);

        //    if(userLanguageLevel == null)
        //        return Result<UserLanguageLevel>.Failure($"UserLanguageLevel with enrollment id {enrollmentId} not found");

        //    return Result<UserLanguageLevel>.Success(userLanguageLevel);
        //}
    }
}

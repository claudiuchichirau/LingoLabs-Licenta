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

        public async Task<Result<IReadOnlyList<UserLanguageLevel>>> FindByLanguageCompetenceIdAsync(Guid languageCompetenceId)
        {
            var userLanguageLevels = await context.UserLanguageLevels
                                .Where(ull => ull.LanguageCompetenceId == languageCompetenceId)
                                .ToListAsync();

            return Result<IReadOnlyList<UserLanguageLevel>>.Success(userLanguageLevels);
        }

        public async Task<Result<IReadOnlyList<UserLanguageLevel>>> FindByLanguageLevelIdAsync(Guid languageLevelId)
        {
            var userLanguageLevels = await context.UserLanguageLevels
                                .Where(ull => ull.LanguageLevelId == languageLevelId)
                                .ToListAsync();

            return Result<IReadOnlyList<UserLanguageLevel>>.Success(userLanguageLevels);
        }

        public async Task<Result<List<UserLanguageLevel>>> GetUserLanguageLevelsByUserIdAsync(Guid userId)
        {
            var userLanguageLevels = await context.UserLanguageLevels
                .Where(ull => ull.Enrollment.UserId == userId)
                .Include(ull => ull.LanguageLevel)
                .Include(ull => ull.LanguageCompetence)
                .ToListAsync();

            return Result<List<UserLanguageLevel>>.Success(userLanguageLevels);
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

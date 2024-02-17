using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class UserLanguageLevelRepository: BaseRepository<UserLanguageLevel>, IUserLanguageLevelRepository
    {
        public UserLanguageLevelRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

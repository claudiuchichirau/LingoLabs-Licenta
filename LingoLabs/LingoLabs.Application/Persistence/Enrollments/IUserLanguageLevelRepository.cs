using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;

namespace LingoLabs.Application.Persistence.Enrollments
{
    public interface IUserLanguageLevelRepository: IAsyncRepository<UserLanguageLevel>
    {
        //Task<Result<UserLanguageLevel>> GetUserLanguageLevelByEnrollmentId(Guid enrollmentId);
    }
}

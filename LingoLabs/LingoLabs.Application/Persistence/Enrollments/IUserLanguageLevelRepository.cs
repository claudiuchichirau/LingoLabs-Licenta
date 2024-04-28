using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;

namespace LingoLabs.Application.Persistence.Enrollments
{
    public interface IUserLanguageLevelRepository: IAsyncRepository<UserLanguageLevel>
    {
        Task<Result<IReadOnlyList<UserLanguageLevel>>> FindByLanguageCompetenceIdAsync(Guid languageCompetenceId);
        Task<Result<IReadOnlyList<UserLanguageLevel>>> FindByLanguageLevelIdAsync(Guid languageLeveleId);
        Task<Result<List<UserLanguageLevel>>> GetUserLanguageLevelsByUserIdAsync(Guid userId);
    }
}

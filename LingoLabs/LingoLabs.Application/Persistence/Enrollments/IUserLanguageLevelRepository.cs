using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Persistence.Enrollments
{
    public interface IUserLanguageLevelRepository: IAsyncRepository<UserLanguageLevel>
    {
        Task<Result<IReadOnlyList<UserLanguageLevel>>> FindByLanguageCompetenceIdAsync(Guid languageCompetenceId);
    }
}

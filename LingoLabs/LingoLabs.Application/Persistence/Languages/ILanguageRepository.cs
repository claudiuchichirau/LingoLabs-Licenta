using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Persistence.Languages
{
    public interface ILanguageRepository : IAsyncRepository<Language>
    {
        Task<bool> ExistsLanguageAsync(string languageName);
        Task<bool> ExistsLanguageForUpdateAsync(string languageName, Guid languageId);
    }
}

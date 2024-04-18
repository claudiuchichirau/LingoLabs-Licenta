using LingoLabs.Domain.Entities.Languages;
namespace LingoLabs.Application.Persistence.Languages
{
    public interface ILanguageLevelRepository: IAsyncRepository<LanguageLevel>
    {
        Task<bool> ExistLanguageLevelAsync(string languageLevelName, Guid languageId);
        Task<bool> ExistLanguageLevelUpdateAsync(string languageLevelName, Guid languageLevelId);
    }
}

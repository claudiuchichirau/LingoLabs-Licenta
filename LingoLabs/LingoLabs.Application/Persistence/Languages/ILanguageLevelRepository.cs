using LingoLabs.Domain.Entities.Languages;
namespace LingoLabs.Application.Persistence.Languages
{
    public interface ILanguageLevelRepository: IAsyncRepository<LanguageLevel>
    {
        Task<bool> ExistLanguageLevelAsync(string languageLevelName);
    }
}

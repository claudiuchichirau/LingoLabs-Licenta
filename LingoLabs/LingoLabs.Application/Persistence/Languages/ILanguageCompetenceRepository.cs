using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Persistence.Languages
{
    public interface ILanguageCompetenceRepository: IAsyncRepository<LanguageCompetence>
    {
        Task<bool> ExistsLanguageCompetenceAsync(LanguageCompetenceType languageCompetenceType);
        Task<LanguageCompetenceType> GetLanguageCompetenceTypeAsync(Guid id);
    }
}

using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Persistence.Languages
{
    public interface ILanguageCompetenceRepository: IAsyncRepository<LanguageCompetence>
    {
        Task<bool> ExistsLanguageCompetenceAsync(LanguageCompetenceType languageCompetenceType, Guid chapterIid);
        Task<LanguageCompetenceType> GetLanguageCompetenceTypeAsync(Guid id);
        Task<bool> ExistsLanguageCompetencePriorityNumberAsync(int priorityNumber, Guid languageCompetenceId);
    }
}

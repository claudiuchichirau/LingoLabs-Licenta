using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Persistence.Languages
{
    public interface IQuestionRepository: IAsyncRepository<Question>
    {
        Task<Result<IReadOnlyList<Question>>> GetQuestionsByLanguageLevelIdAsync(Guid languageLevelId);
        Task<Result<IReadOnlyList<Question>>> GetQuestionsByLanguageCompetenceIdAsync(Guid languageCompetenceId);
        Task<bool> ExistsQuestionPriorityNumberAsync(int priorityNumber, Guid questionId);
    }
}

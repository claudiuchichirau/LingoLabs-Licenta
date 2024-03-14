using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Persistence.Languages
{
    public interface IChapterRepository: IAsyncRepository<Chapter>
    {
        Task<bool> ExistChapterByNameAsync(string name);
        Task<bool> ExistChapterByNameForUpdateAsync(string chapterName, Guid chapterId);
        Task<bool> ExistsChapterPriorityNumberAsync(int priorityNumber, Guid chapterId);
    }
}

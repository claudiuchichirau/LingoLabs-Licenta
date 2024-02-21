using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Persistence.Languages
{
    public interface IChapterRepository: IAsyncRepository<Chapter>
    {
        Task<bool> ExistChapterByNameAsync(string name);
    }
}

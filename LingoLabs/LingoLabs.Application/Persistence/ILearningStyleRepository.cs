using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Persistence
{
    public interface ILearningStyleRepository: IAsyncRepository<LearningStyle>
    {
        Task<bool> ExistsLearningStyleAsync(LearningType type);
    }
}

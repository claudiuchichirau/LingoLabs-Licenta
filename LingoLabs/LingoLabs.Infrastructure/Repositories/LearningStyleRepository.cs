using LingoLabs.Domain.Entities;
using LingoLabs.Application.Persistence;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories
{
    public class LearningStyleRepository: BaseRepository<LearningStyle>, ILearningStyleRepository
    {
        public LearningStyleRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

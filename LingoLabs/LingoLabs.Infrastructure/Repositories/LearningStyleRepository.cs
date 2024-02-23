using LingoLabs.Domain.Entities;
using LingoLabs.Application.Persistence;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories
{
    public class LearningStyleRepository: BaseRepository<LearningStyle>, ILearningStyleRepository
    {
        public LearningStyleRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsLearningStyleAsync(LearningType type)
        {
            return await context.LearningStyles.AnyAsync(learningStyle => learningStyle.LearningType == type);
        }
    }
}

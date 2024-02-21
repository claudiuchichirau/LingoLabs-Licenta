using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class WordPairRepository: BaseRepository<WordPair>, IWordPairRepository
    {
        public WordPairRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<WordPair>> FindByIdAsync(Guid id)
        {
            var result = await context.WordPairs
                .Include(l => l.MatchingWordsQuestion)
                .FirstOrDefaultAsync(l => l.WordPairId == id);

            if (result == null)
            {
                return Result<WordPair>.Failure($"Entity with id {id} not found");
            }
            return Result<WordPair>.Success(result);
        }
    }
}

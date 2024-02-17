using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class WordPairRepository: BaseRepository<WordPair>, IWordPairRepository
    {
        public WordPairRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

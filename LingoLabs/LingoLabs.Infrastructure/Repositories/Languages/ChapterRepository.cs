using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class ChapterRepository: BaseRepository<Chapter>, IChapterRepository
    {
        public ChapterRepository(LingoLabsDbContext context) : base(context)
        {

        }
    }
}

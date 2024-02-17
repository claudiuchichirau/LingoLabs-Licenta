using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class ChapterResultRepository: BaseRepository<ChapterResult>, IChapterResultRepository
    {
        public ChapterResultRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

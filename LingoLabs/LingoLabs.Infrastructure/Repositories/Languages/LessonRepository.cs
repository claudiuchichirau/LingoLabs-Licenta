using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class LessonRepository: BaseRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

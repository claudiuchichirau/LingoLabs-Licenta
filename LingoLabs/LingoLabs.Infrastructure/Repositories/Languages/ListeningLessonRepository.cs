using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class ListeningLessonRepository: BaseRepository<ListeningLesson>, IListeningLessonRepository
    {
        public ListeningLessonRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

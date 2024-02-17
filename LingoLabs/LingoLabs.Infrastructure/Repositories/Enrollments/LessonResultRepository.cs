using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class LessonResultRepository: BaseRepository<LessonResult>, ILessonResultRepository
    {  
        public LessonResultRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

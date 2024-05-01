using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;

namespace LingoLabs.Application.Persistence.Enrollments
{
    public interface ILessonResultRepository: IAsyncRepository<LessonResult>
    {
        Task<List<LessonResult>> GetLessonResultsByLessonId(Guid lessonId);
    }
}

using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Persistence.Languages
{
    public interface ILessonRepository: IAsyncRepository<Lesson>
    {
        Task<bool> ExistsLessonAsync(string lessonTitle);

        Task<bool> ExistsLessonForUpdateAsync(string lessonTitle, Guid lessonId);
        Task<bool> ExistsLessonPriorityNumberAsync(int priorityNumber, Guid lessonId);
    }
}

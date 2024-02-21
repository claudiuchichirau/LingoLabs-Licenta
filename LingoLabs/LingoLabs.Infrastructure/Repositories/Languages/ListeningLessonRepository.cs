using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class ListeningLessonRepository: BaseRepository<ListeningLesson>, IListeningLessonRepository
    {
        public ListeningLessonRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<ListeningLesson>> FindByIdAsync(Guid id)
        {
            var result = await context.ListeningLessons
                .Include(l => l.LessonQuestions)
                .Include(l => l.LanguageCompetence)
                .FirstOrDefaultAsync(l => l.LessonId == id);

            if (result == null)
            {
                return Result<ListeningLesson>.Failure($"Entity with id {id} not found");
            }

            return Result<ListeningLesson>.Success(result);
        }
    }
}

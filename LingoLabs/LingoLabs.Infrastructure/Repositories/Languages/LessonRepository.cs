using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class LessonRepository: BaseRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<Lesson>> FindByIdAsync(Guid id)
        {
            var result = await context.Lessons
                .Include(lesson => lesson.LessonQuestions)
                .Include(lesson => lesson.LanguageCompetence)
                .FirstOrDefaultAsync(lesson => lesson.LessonId == id);

            if (result == null)
            {
                return Result<Lesson>.Failure($"Entity with id {id} not found");
            }

            return Result<Lesson>.Success(result);
        }
    }
}

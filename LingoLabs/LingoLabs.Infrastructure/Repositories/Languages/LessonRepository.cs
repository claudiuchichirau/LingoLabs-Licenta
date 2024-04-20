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
                .ThenInclude(question => question.Choices)
                .Include(lesson => lesson.Chapter)
                .FirstOrDefaultAsync(lesson => lesson.LessonId == id);

            if (result == null)
            {
                return Result<Lesson>.Failure($"Entity with id {id} not found");
            }

            return Result<Lesson>.Success(result);
        }

        public async Task<bool> ExistsLessonAsync(string lessonTitle)
        {
            return await context.Lessons.AnyAsync(lesson => lesson.LessonTitle == lessonTitle);
        }

        public async Task<bool> ExistsLessonForUpdateAsync(string lessonTitle, Guid lessonId)
        {
            var lesson = await context.Lessons.FirstOrDefaultAsync(l => l.LessonId == lessonId);

            var duplicateLesson = await context.Lessons
                .AnyAsync(l => l.LessonTitle == lessonTitle && l.LessonId != lessonId);

            if (!duplicateLesson)
                return false;
            return true;
        }

        public async Task<bool> ExistsLessonPriorityNumberAsync(int priorityNumber, Guid lessonId)
        {
            var lessonFound = await context.Lessons.FirstOrDefaultAsync(lesson => lesson.LessonId == lessonId);

            return await context.Lessons
                .AnyAsync(lesson => lesson.LessonPriorityNumber == priorityNumber && lesson.ChapterId == lessonFound.ChapterId && lesson.LessonId != lessonFound.LessonId);
        }
    }
}

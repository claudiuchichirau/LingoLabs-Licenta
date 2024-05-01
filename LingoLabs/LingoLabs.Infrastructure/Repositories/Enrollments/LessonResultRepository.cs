using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class LessonResultRepository: BaseRepository<LessonResult>, ILessonResultRepository
    {  
        public LessonResultRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<LessonResult>> FindByIdAsync(Guid id)
        {
            var lessonResult = await context.LessonResults
                .Include(lr => lr.QuestionResults)
                .Include(lr => lr.Lesson)
                .Include(lr => lr.ChapterResult)
                .FirstOrDefaultAsync(lr => lr.LessonResultId == id);

            if(lessonResult == null)
                return Result<LessonResult>.Failure($"LessonResult with id {id} not found");

            return Result<LessonResult>.Success(lessonResult);
        }

        public async Task<List<LessonResult>> GetLessonResultsByLessonId(Guid lessonId)
        {
            return await context.LessonResults
                .Include(lr => lr.QuestionResults)
                .Include(lr => lr.Lesson)
                .Include(lr => lr.ChapterResult)
                .Where(lr => lr.LessonId == lessonId)
                .ToListAsync();
        }
    }
}

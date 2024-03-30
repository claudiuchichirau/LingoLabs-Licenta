using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class ChapterResultRepository: BaseRepository<ChapterResult>, IChapterResultRepository
    {
        public ChapterResultRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<ChapterResult>> FindByIdAsync(Guid id)
        {
            var chapterResult = await context.ChapterResults
                .Include(cr => cr.Chapter)
                .Include(cr => cr.LessonResults)
                .Include(cr => cr.LanguageLevelResult)
                .FirstOrDefaultAsync(cr => cr.ChapterResultId == id);

            if(chapterResult == null)
                return Result<ChapterResult>.Failure($"ChapterResult with id {id} not found");

            return Result<ChapterResult>.Success(chapterResult);
        }
    }
}

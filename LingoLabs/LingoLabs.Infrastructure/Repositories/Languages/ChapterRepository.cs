using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class ChapterRepository: BaseRepository<Chapter>, IChapterRepository
    {
        public ChapterRepository(LingoLabsDbContext context) : base(context)
        {

        }

        public override async Task<Result<Chapter>> FindByIdAsync(Guid id)
        {
            var chapter = await context.Chapters
                .Include(c => c.languageCompetences)
                .Include(c => c.ChapterKeyWords)
                .FirstOrDefaultAsync(c => c.ChapterId == id);

            if (chapter == null)
            {
                return Result<Chapter>.Failure($"Chapter with id {id} not found");
            }

            return Result<Chapter>.Success(chapter);
        }

        public async Task<bool> ExistChapterByNameAsync(string name)
        {
            return await context.Chapters.AnyAsync(c => c.ChapterName == name);
        }

        public async Task<bool> ExistChapterByNameForUpdateAsync(string chapterName, Guid chapterId)
        {
            var chapter = await context.Chapters.FirstOrDefaultAsync(l => l.ChapterId == chapterId);

            var duplicateLanguage = await context.Chapters
                .AnyAsync(l => l.ChapterName == chapterName && l.ChapterId != chapterId);

            if (!duplicateLanguage)
                return false;
            return true;
        }
    }
}

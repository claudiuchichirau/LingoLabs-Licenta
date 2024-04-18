using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class LanguageLevelRepository: BaseRepository<LanguageLevel>, ILanguageLevelRepository
    {
        public LanguageLevelRepository(LingoLabsDbContext context) : base(context)
        {

        }

        public override async Task<Result<LanguageLevel>> FindByIdAsync(Guid id)
        {
            var result = await context.LanguageLevels
                .Include(level => level.LanguageLevelChapters)
                .Include(level => level.LanguageLevelTags)
                .Include(level => level.Language)
                .FirstOrDefaultAsync(level => level.LanguageLevelId == id);

            if (result == null)
            {
                return Result<LanguageLevel>.Failure($"Entity with id {id} not found");
            }

            return Result<LanguageLevel>.Success(result);
        }

        public async Task<bool> ExistLanguageLevelAsync(string languageLevelName, Guid languageId)
        {
            return await context.LanguageLevels.AnyAsync(languageLevel => languageLevel.LanguageLevelName == languageLevelName && languageLevel.LanguageId == languageId);
        }

        public async Task<bool> ExistLanguageLevelUpdateAsync(string languageLevelName, Guid languageLevelId)
        {
            var languageLevel = await context.LanguageLevels.FirstOrDefaultAsync(l => l.LanguageLevelId == languageLevelId);

            var duplicateLanguageLevel = await context.LanguageLevels
                .AnyAsync(l => l.LanguageLevelName == languageLevelName && l.LanguageLevelId != languageLevelId);

            if (!duplicateLanguageLevel)
                return false;
            return true;
        }
    }
}

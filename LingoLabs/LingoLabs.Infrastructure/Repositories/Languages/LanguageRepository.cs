using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<Language>> FindByIdAsync(Guid id)
        {

            var language = await context.Languages
                .Include(language => language.LanguageLevels)
                .Include(language => language.LanguageCompetences)
                .Include(language => language.LanguageTags)
                .Include(language => language.PlacementTest)
                .FirstOrDefaultAsync(language => language.LanguageId == id);

            if (language == null)
            {
                return Result<Language>.Failure("Language not found");
            }

            return Result<Language>.Success(language);
        }

        public async Task<bool> ExistsLanguageAsync(string languageName)
        {
            return await context.Languages.AnyAsync(language => language.LanguageName == languageName);
        }

        public async Task<bool> ExistsLanguageForUpdateAsync(string languageName, Guid languageId)
        {
            var language = await context.Languages.FirstOrDefaultAsync(l => l.LanguageId == languageId);

            var duplicateLanguage = await context.Languages
                .AnyAsync(l => l.LanguageName == languageName && l.LanguageId != languageId);

            if(!duplicateLanguage)
                return false;
            return true;
        }

        public async Task<bool> ExistsLanguageLevelPriorityNumberAsync(int priorityNumber, Guid languageLevelId)
        {
            var languageLevelFound = await context.LanguageLevels.FirstOrDefaultAsync(languageLevel => languageLevel.LanguageLevelId == languageLevelId);
            
            return await context.LanguageLevels
                .AnyAsync(languageLevel => languageLevel.PriorityNumber == priorityNumber && languageLevel.LanguageId == languageLevelFound.LanguageId && languageLevel.LanguageLevelId != languageLevelFound.LanguageLevelId);
        }
    }
}

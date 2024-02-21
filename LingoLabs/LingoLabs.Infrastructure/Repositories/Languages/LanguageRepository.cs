﻿using LingoLabs.Application.Persistence.Languages;
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
                .Include(language => language.LanguageKeyWords)
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
    }
}

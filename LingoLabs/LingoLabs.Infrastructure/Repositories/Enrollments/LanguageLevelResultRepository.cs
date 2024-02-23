using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class LanguageLevelResultRepository: BaseRepository<LanguageLevelResult>, ILanguageLevelResultRepository
    {
        public LanguageLevelResultRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<LanguageLevelResult>> FindByIdAsync(Guid id)
        {
            var languageLevelResult = await context.LanguageLevelResults
                .Include(llr => llr.ChapterResults)
                .Include(llr => llr.LanguageLevel)
                .Include(llr => llr.Enrollment)
                .FirstOrDefaultAsync(llr => llr.LanguageLevelResultId == id);

            if(languageLevelResult == null)
                return Result<LanguageLevelResult>.Failure($"LanguageLevelResult with id {id} not found");

            return Result<LanguageLevelResult>.Success(languageLevelResult);
        }
    }
}

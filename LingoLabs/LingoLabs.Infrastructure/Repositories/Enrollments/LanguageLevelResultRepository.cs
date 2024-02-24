using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Domain.Entities.Languages;
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

        public async Task<bool> CheckLanguageLevel(Guid enrollmentId, Guid languageLevelId)
        {
            var enrollment = await context.Enrollments.FindAsync(enrollmentId);
            if (enrollment == null)
            {
                return false;
            }

            var language = enrollment.Language;
            if (language == null)
            {
                return false;
            }

            return language.LanguageLevels.Any(ll => ll.LanguageLevelId == languageLevelId);
        }

    }
}

using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class LanguageCompetenceResultRepository: BaseRepository<LanguageCompetenceResult>, ILanguageCompetenceResultRepository
    {
        public LanguageCompetenceResultRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<LanguageCompetenceResult>> FindByIdAsync(Guid id)
        {
            var languageCompetenceResult = await context.LanguageCompetenceResults
                .Include(lcr => lcr.LessonsResults)
                .Include(lcr => lcr.ChapterResult)
                .Include(lcr => lcr.LanguageCompetence)
                .FirstOrDefaultAsync(lcr => lcr.LanguageCompetenceResultId == id);

            if(languageCompetenceResult == null)
                return Result<LanguageCompetenceResult>.Failure($"LanguageCompetenceResult with id {id} not found");

            return Result<LanguageCompetenceResult>.Success(languageCompetenceResult);
        }
    }
}

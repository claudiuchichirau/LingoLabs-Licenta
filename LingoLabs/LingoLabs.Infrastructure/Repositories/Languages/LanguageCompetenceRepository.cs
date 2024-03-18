using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class LanguageCompetenceRepository : BaseRepository<LanguageCompetence>, ILanguageCompetenceRepository
    {
        public LanguageCompetenceRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<LanguageCompetence>> FindByIdAsync(Guid id)
        {
            var result = await context.LanguageCompetences
                .Include(competence => competence.Lessons)
                .Include(competence => competence.LearningCompetenceTags)
                .Include(competence => competence.Language)
                .Include(competence => competence.Chapter)
                .FirstOrDefaultAsync(competence => competence.LanguageCompetenceId == id);

            if (result == null)
            {
                return Result<LanguageCompetence>.Failure($"Entity with id {id} not found");
            }

            return Result<LanguageCompetence>.Success(result);
        }

        public async Task<bool> ExistsLanguageCompetenceAsync(LanguageCompetenceType languageCompetenceType, Guid chapterId)
        {
            return await context.LanguageCompetences.AnyAsync(competence => competence.LanguageCompetenceType == languageCompetenceType && competence.ChapterId == chapterId);
        }

        public async Task<LanguageCompetenceType> GetLanguageCompetenceTypeAsync(Guid id)
        {
            return await context.LanguageCompetences
                .Where(competence => competence.LanguageCompetenceId == id)
                .Select(competence => competence.LanguageCompetenceType)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsLanguageCompetencePriorityNumberAsync(int priorityNumber, Guid languageCompetenceId)
        {
            var languageCompetenceFound = await context.LanguageCompetences.FirstOrDefaultAsync(languageCompetence => languageCompetence.LanguageCompetenceId == languageCompetenceId);

            return await context.LanguageCompetences
                .AnyAsync(languageCompetence => languageCompetence.LanguageCompetencePriorityNumber == priorityNumber && languageCompetence.ChapterId == languageCompetenceFound.ChapterId && languageCompetence.LanguageCompetenceId != languageCompetenceFound.LanguageCompetenceId);
        }
    }
}

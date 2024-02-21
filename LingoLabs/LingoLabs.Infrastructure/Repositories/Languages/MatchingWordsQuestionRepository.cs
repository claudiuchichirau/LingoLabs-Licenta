using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class MatchingWordsQuestionRepository: BaseRepository<MatchingWordsQuestion>, IMatchingWordsQuestionRepository
    {
        public MatchingWordsQuestionRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<MatchingWordsQuestion>> FindByIdAsync(Guid id)
        {
            var result = await context.MatchingWordsQuestions
                .Include(l => l.QuestionChoices)
                .Include(l => l.Lesson)
                .Include(l => l.WordPairs)
                .FirstOrDefaultAsync(l => l.QuestionId == id);

            if (result == null)
            {
                return Result<MatchingWordsQuestion>.Failure($"Entity with id {id} not found");
            }
            return Result<MatchingWordsQuestion>.Success(result);
        }
    }
}

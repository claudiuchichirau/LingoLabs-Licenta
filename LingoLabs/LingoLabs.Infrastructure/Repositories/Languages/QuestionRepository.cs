using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<Question>> FindByIdAsync(Guid id)
        {
            var result = await context.Questions
                .Include(l => l.QuestionChoices)
                .Include(l => l.Lesson)
                .FirstOrDefaultAsync(l => l.QuestionId == id);

            if (result == null)
            {
                return Result<Question>.Failure($"Entity with id {id} not found");
            }
            return Result<Question>.Success(result);
        }
    }
}

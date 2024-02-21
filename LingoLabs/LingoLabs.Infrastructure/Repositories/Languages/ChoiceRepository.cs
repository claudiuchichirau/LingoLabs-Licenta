using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class ChoiceRepository: BaseRepository<Choice>, IChoiceRepository
    {
        public ChoiceRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<Choice>> FindByIdAsync(Guid id)
        {
            var result = await context.Choices
                .Include(c => c.Question)
                .FirstOrDefaultAsync(c => c.ChoiceId == id);

            if (result == null)
            {
                return Result<Choice>.Failure($"Entity with id {id} not found");
            }

            return Result<Choice>.Success(result);
        }
    }
}

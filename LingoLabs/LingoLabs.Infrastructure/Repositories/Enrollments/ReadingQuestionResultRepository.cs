using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class ReadingQuestionResultRepository: BaseRepository<ReadingQuestionResult>, IReadingQuestionResultRepository
    {
        public ReadingQuestionResultRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<ReadingQuestionResult>> FindByIdAsync(Guid id)
        {
            var result = await context.ReadingQuestionResults
                .Include(x => x.LessonResult)
                .Include(x => x.Question)
                .FirstOrDefaultAsync(x => x.QuestionResultId == id);

            if (result == null)
                return Result<ReadingQuestionResult>.Failure($"ReadingQuestionResult with id {id} not found");
            
            return Result<ReadingQuestionResult>.Success(result);
        }
    }
}

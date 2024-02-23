using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class WritingQuestionResultRepository: BaseRepository<WritingQuestionResult>, IWritingQuestionResultRepository
    {
        public WritingQuestionResultRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<WritingQuestionResult>> FindByIdAsync(Guid id)
        {
            var writingQuestionResult = await context.WritingQuestionResults
                .Include(wqr => wqr.Question)
                .Include(wqr => wqr.LessonResult)
                .FirstOrDefaultAsync(wqr => wqr.QuestionResultId == id);

            if (writingQuestionResult == null)
                return Result<WritingQuestionResult>.Failure($"No WritingQuestionResult found for id {id}");

            return Result<WritingQuestionResult>.Success(writingQuestionResult);
        }
    }
}

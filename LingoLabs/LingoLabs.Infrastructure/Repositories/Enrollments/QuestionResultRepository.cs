using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class QuestionResultRepository: BaseRepository<QuestionResult>, IQuestionResultRepository
    {
        public QuestionResultRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<QuestionResult>> FindByIdAsync(Guid id)
        {
            var questionResult = await context.QuestionResults
                .Include(qr => qr.Question)
                .Include(qr => qr.LessonResult)
                .FirstOrDefaultAsync(qr => qr.QuestionResultId == id);
            
            if(questionResult == null)
                return Result<QuestionResult>.Failure($"QuestionResult with id {id} not found");

            return Result<QuestionResult>.Success(questionResult);
        }
    }
}

using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class WritingQuestionResultRepository: BaseRepository<WritingQuestionResult>, IWritingQuestionResultRepository
    {
        public WritingQuestionResultRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

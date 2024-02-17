using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class ReadingQuestionResultRepository: BaseRepository<ReadingQuestionResult>, IReadingQuestionResultRepository
    {
        public ReadingQuestionResultRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

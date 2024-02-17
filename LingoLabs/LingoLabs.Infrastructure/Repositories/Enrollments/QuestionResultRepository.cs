using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class QuestionResultRepository: BaseRepository<QuestionResult>, IQuestionResultRepository
    {
        public QuestionResultRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

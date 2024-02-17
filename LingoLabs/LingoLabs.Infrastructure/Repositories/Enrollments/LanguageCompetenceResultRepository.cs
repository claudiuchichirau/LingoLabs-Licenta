using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class LanguageCompetenceResultRepository: BaseRepository<LanguageCompetenceResult>, ILanguageCompetenceResultRepository
    {
        public LanguageCompetenceResultRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

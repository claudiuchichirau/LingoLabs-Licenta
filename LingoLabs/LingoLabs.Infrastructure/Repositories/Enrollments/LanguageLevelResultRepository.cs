using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Enrollments
{
    public class LanguageLevelResultRepository: BaseRepository<LanguageLevelResult>, ILanguageLevelResultRepository
    {
        public LanguageLevelResultRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

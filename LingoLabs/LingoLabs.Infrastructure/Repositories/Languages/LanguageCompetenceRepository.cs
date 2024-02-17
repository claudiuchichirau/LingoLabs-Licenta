using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class LanguageCompetenceRepository: BaseRepository<LanguageCompetence>, ILanguageCompetenceRepository
    {
        public LanguageCompetenceRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

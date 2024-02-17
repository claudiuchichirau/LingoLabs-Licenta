using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

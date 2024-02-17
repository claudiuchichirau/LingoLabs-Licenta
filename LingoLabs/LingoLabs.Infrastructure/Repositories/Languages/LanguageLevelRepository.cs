using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class LanguageLevelRepository: BaseRepository<LanguageLevel>, ILanguageLevelRepository
    {
        public LanguageLevelRepository(LingoLabsDbContext context) : base(context)
        {

        }
    }
}

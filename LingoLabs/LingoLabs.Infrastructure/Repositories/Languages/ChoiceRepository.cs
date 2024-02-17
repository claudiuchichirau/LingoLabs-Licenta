using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class ChoiceRepository: BaseRepository<Choice>, IChoiceRepository
    {
        public ChoiceRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class TagRepository: BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

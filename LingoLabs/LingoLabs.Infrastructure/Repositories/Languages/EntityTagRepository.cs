using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class EntityTagRepository : BaseRepository<EntityTag>, IEntityTagRepository
    {
        public EntityTagRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

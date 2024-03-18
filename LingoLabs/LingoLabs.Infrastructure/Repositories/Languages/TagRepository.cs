using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class TagRepository: BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public async Task<Result<IReadOnlyList<Tag>>> GetTagsWithoutEntityTagForLanguageId(Guid languageId)
        {
            var tagsWithEntityTag = await context.EntityTags
                .Where(et => et.LanguageId == languageId)
                .Select(et => et.Tag)
                .ToListAsync();

            var allTags = await context.Tags.ToListAsync();

            var tagsWithoutEntityTag = allTags.Except(tagsWithEntityTag).ToList();

            return Result<IReadOnlyList<Tag>>.Success(tagsWithoutEntityTag);
        }

    }
}

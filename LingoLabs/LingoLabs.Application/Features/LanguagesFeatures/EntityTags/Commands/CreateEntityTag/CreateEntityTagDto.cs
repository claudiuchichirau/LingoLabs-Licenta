using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.CreateEntityTag
{
    public class CreateEntityTagDto
    {
        public Guid EntityTagId { get; set; }
        public Guid EntityId { get; set; }
        public Guid TagId { get; set; }
        public EntityType EntityType { get; set; }
    }
}

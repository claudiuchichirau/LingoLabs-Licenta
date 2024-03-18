using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Tag : AuditableEntity
    {
        public Guid TagId { get; private set; }
        public string TagContent { get; private set; }
        public List<EntityTag> EntityTags { get; private set; } = [];

        private Tag(string tagContent)
        {
            TagId = Guid.NewGuid();
            TagContent = tagContent;
        }

        public static Result<Tag> Create(string tagContent)
        {
            if (string.IsNullOrEmpty(tagContent))
            {
                return Result<Tag>.Failure("Content is required");
            }

            return Result<Tag>.Success(new Tag(tagContent));
        }

        public void AttachEntityTag(EntityTag entityTag)
        {
            if (entityTag != null)
            {
                if (EntityTags == null)
                    EntityTags = new List<EntityTag> { entityTag };
                else
                    EntityTags.Add(entityTag);
            }
        }
    }
}

using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Tag : AuditableEntity
    {
        public Guid TagId { get; private set; }
        public string TagContent { get; private set; }

        private Tag(string content)
        {
            TagId = Guid.NewGuid();
            TagContent = content;
        }

        public static Result<Tag> Create(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return Result<Tag>.Failure("Content is required");
            }

            return Result<Tag>.Success(new Tag(content));
        }
    }
}

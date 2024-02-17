using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Domain.Entities
{
    public class LearningStyle : AuditableEntity
    {
        public Guid LearningStyleId { get; private set; }
        public string LearningStyleName { get; private set; }
        public string? LearningStyleDescription { get; private set; }
        public List<Tag>? LearningStyleKeyWords { get; private set; } = new ();
        public LearningType LearningType { get; private set; }

        private LearningStyle(string learningStyleName, LearningType learningType)
        {
            LearningStyleId = Guid.NewGuid();
            LearningStyleName = learningStyleName;
            LearningType = learningType;
        }

        public static Result<LearningStyle> Create(string learningStyleName, LearningType learningType)
        {
            if (string.IsNullOrWhiteSpace(learningStyleName))
                return Result<LearningStyle>.Failure("LearningStyleName is required");

            if (!IsValidLearningType(learningType))
                return Result<LearningStyle>.Failure("Invalid LearningType");

            return Result<LearningStyle>.Success(new LearningStyle(learningStyleName, learningType));
        }

        public static bool IsValidLearningType(LearningType learningType)
        {
            return learningType == LearningType.Auditory ||
                   learningType == LearningType.Visual ||
                   learningType == LearningType.Kinesthetic ||
                   learningType == LearningType.Logical;
        }

        public void AttachDescription(string learningStyleDescription)
        {
            if (!string.IsNullOrWhiteSpace(learningStyleDescription))
                LearningStyleDescription = learningStyleDescription;
        }

        public void AttachKeyWord(Tag tag)
        {
            if (tag != null)
            {
                if (LearningStyleKeyWords == null)
                    LearningStyleKeyWords = new List<Tag> { tag };
                else
                    LearningStyleKeyWords.Add(tag);
            }
        }

        public void UpdateLearningStyle(string learningStyleName, string learningStyleDescription)
        {
            if (string.IsNullOrWhiteSpace(learningStyleName))
                LearningStyleName = learningStyleName;
            if (!string.IsNullOrWhiteSpace(learningStyleDescription))
                LearningStyleDescription = learningStyleDescription;
        }


    }
}

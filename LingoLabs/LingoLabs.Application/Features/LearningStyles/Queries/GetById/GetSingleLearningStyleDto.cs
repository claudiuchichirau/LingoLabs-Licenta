using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;

namespace LingoLabs.Application.Features.LearningStyles.Queries.GetById
{
    public class GetSingleLearningStyleDto: LearningStyleDto
    {
        public string LearningStyleDescription { get; set; } = string.Empty;
        public List<TagDto> LearningStyleKeyWords { get; set; } = [];
    }
}

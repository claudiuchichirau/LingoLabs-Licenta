using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;
using LingoLabs.Application.Persistence;
using MediatR;

namespace LingoLabs.Application.Features.LearningStyles.Queries.GetById
{
    public class GetByIdLearningStyleQueryHandler : IRequestHandler<GetByIdLearningStyleQuery, GetSingleLearningStyleDto>
    {
        private readonly ILearningStyleRepository learningStyleRepository;

        public GetByIdLearningStyleQueryHandler(ILearningStyleRepository learningStyleRepository)
        {
            this.learningStyleRepository = learningStyleRepository;
        }
        public async Task<GetSingleLearningStyleDto> Handle(GetByIdLearningStyleQuery request, CancellationToken cancellationToken)
        {
            var learningStyle = await learningStyleRepository.FindByIdAsync(request.Id);
            if(learningStyle.IsSuccess)
            {
                return new GetSingleLearningStyleDto 
                {
                    LearningStyleId = learningStyle.Value.LearningStyleId,
                    LearningStyleName = learningStyle.Value.LearningStyleName,
                    LearningStyleDescription = learningStyle.Value.LearningStyleDescription,
                    LearningType = learningStyle.Value.LearningType

                    //LearningStyleKeyWords = learningStyle.Value.LearningStyleKeyWords.Select(tag => new TagDto
                    //{
                    //    TagId = tag.TagId,
                    //    TagContent = tag.TagContent
                    //}).ToList()

                };
            }

            return new GetSingleLearningStyleDto();
        }
    }
}

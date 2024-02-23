using LingoLabs.Application.Persistence;
using MediatR;

namespace LingoLabs.Application.Features.LearningStyles.Queries.GetAll
{
    public class GetAllLearningStylesQueryHandler : IRequestHandler<GetAllLearningStylesQuery, GetAllLearningStylesResponse>
    {
        private readonly ILearningStyleRepository learningStyleRepository;

        public GetAllLearningStylesQueryHandler(ILearningStyleRepository learningStyleRepository)
        {
            this.learningStyleRepository = learningStyleRepository;
        }
        public async Task<GetAllLearningStylesResponse> Handle(GetAllLearningStylesQuery request, CancellationToken cancellationToken)
        {
            GetAllLearningStylesResponse response = new();
            var result = await learningStyleRepository.GetAllAsync();
            if(result.IsSuccess)
            {
                response.LearningStyles = result.Value.Select(learningStyle => new LearningStyleDto
                {
                    LearningStyleId = learningStyle.LearningStyleId,
                    LearningStyleName = learningStyle.LearningStyleName,
                    LearningType = learningStyle.LearningType
                }).ToList();
            }

            return response;
        }
    }
}

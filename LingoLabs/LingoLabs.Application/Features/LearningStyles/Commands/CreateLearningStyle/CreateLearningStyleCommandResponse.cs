using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LearningStyles.Commands.CreateLearningStyle
{
    public class CreateLearningStyleCommandResponse: BaseResponse
    {
        public CreateLearningStyleCommandResponse() : base()
        {
        }

        public CreateLearningStyleDto LearningStyle { get; set; }
    }
}

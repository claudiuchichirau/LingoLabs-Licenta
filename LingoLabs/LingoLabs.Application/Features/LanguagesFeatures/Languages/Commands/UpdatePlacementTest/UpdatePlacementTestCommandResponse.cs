using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdatePlacementTest
{
    public class UpdatePlacementTestCommandResponse: BaseResponse
    {
        public UpdatePlacementTestCommandResponse() : base()
        {
        }

        public UpdatePlacementTestDto UpdatePlacementTestDto { get; set; }
    }
}

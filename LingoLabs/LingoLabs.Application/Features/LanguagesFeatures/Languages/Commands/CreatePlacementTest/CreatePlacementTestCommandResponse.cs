using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreatePlacementTest
{
    public class CreatePlacementTestCommandResponse: BaseResponse
    {
        public CreatePlacementTestCommandResponse() : base()
        {
        }

        public CreatePlacementTestDto CreatePlacementTestDto { get; set; }
    }
}

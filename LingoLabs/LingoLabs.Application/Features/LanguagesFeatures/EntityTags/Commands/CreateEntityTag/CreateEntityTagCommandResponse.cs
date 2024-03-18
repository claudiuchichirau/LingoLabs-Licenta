using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.CreateEntityTag
{
    public class CreateEntityTagCommandResponse: BaseResponse
    {
        public CreateEntityTagCommandResponse() : base()
        {
        }

        public CreateEntityTagDto EntityTag { get; set; } = default!;
    }
}

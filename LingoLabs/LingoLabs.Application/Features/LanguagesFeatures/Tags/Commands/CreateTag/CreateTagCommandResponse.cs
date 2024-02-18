using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Tags.Commands.CreateTag
{
    public class CreateTagCommandResponse: BaseResponse
    {
        public CreateTagCommandResponse(): base()
        {
        }

        public CreateTagDto Tag { get; set; }
    }
}

using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandResponse : BaseResponse
    {
        public CreateLanguageCommandResponse() : base()
        {
        }

        public CreateLanguageDto Language { get; set; } = default!;
    }
}
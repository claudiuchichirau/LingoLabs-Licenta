using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Commands.CreateWordPair
{
    public class CreateWordPairCommandResponse: BaseResponse
    {
        public CreateWordPairCommandResponse() : base()
        {
        }
        
        public CreateWordPairDto WordPair { get; set; }
    }
}

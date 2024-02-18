using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Commands.CreateWordPair
{
    public class CreateWordPairCommand: IRequest<CreateWordPairCommandResponse>
    {
        public string KeyWord { get; set; } = default!;
        public string ValueWord { get; set; } = default!;
    }
}

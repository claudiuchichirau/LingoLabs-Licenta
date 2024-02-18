using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Tags.Commands.CreateTag
{
    public class CreateTagCommand: IRequest<CreateTagCommandResponse>
    {
        public string TagContent { get; set; } = default!;
    }
}

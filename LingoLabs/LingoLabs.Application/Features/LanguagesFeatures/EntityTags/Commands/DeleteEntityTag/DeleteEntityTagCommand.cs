using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.DeleteEntityTag
{
    public class DeleteEntityTagCommand: IRequest<DeleteEntityTagCommandResponse>
    {
        public Guid EntityTagId { get; set; }
    }
}

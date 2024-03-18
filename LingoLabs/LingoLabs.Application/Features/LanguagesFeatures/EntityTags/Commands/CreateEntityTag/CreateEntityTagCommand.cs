using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.CreateEntityTag
{
    public class CreateEntityTagCommand: IRequest<CreateEntityTagCommandResponse>
    {
        public Guid EntityId { get; set; }
        public Guid TagId { get; set; }
        public EntityType EntityType { get; set; }
    }
}

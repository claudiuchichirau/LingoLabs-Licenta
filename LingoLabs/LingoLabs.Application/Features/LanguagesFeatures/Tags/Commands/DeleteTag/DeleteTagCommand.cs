using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Tags.Commands.DeleteTag
{
    public class DeleteTagCommand: IRequest<DeleteTagCommandResponse>
    {
        public Guid TagId { get; set; }
    }
}

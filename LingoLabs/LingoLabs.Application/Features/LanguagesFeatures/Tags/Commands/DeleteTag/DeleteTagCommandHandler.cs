using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, DeleteTagCommandResponse>
    {
        private readonly ITagRepository tagRepository;

        public DeleteTagCommandHandler(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        public async Task<DeleteTagCommandResponse> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteTagCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new DeleteTagCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var tag = await tagRepository.FindByIdAsync(request.TagId);

            if(!tag.IsSuccess)
            {
                return new DeleteTagCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { tag.Error }
                };
            }

            await tagRepository.DeleteAsync(request.TagId);

            return new DeleteTagCommandResponse
            { 
                Success = true 
            };
        }
    }
}

using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.DeleteEntityTag
{
    public class DeleteEntityTagCommandHandler : IRequestHandler<DeleteEntityTagCommand, DeleteEntityTagCommandResponse>
    {
        private readonly IEntityTagRepository entityTagRepository;

        public DeleteEntityTagCommandHandler(IEntityTagRepository entityTagRepository)
        {
            this.entityTagRepository = entityTagRepository;
        }
        public async Task<DeleteEntityTagCommandResponse> Handle(DeleteEntityTagCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteEntityTagCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid) 
            {
                return new DeleteEntityTagCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var entityTag = await entityTagRepository.FindByIdAsync(request.EntityTagId);

            if(!entityTag.IsSuccess)
            {
                return new DeleteEntityTagCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { entityTag.Error }
                };
            }

            await entityTagRepository.DeleteAsync(request.EntityTagId);

            return new DeleteEntityTagCommandResponse
            {
                Success = true
            };
        }
    }
}

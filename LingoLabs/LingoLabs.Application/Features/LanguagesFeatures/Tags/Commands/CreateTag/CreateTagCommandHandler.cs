using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler: IRequestHandler<CreateTagCommand, CreateTagCommandResponse>
    {
        private readonly ITagRepository repository;

        public CreateTagCommandHandler(ITagRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateTagCommandResponse> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTagCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateTagCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var tag = Tag.Create(request.TagContent);
            if (tag.IsSuccess)
            {
                await repository.AddAsync(tag.Value);
                return new CreateTagCommandResponse
                {
                    Tag = new CreateTagDto
                    {
                        TagId = tag.Value.TagId,
                        TagContent = tag.Value.TagContent
                    }
                };
            }

            return new CreateTagCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { tag.Error }
            };
        }
    }
}

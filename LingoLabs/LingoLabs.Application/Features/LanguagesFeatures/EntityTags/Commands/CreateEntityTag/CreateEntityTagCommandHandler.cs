using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.CreateEntityTag
{
    public class CreateEntityTagCommandHandler : IRequestHandler<CreateEntityTagCommand, CreateEntityTagCommandResponse>
    {
        private readonly IEntityTagRepository entityTagRepository;

        public CreateEntityTagCommandHandler(IEntityTagRepository entityTagRepository)
        {
            this.entityTagRepository = entityTagRepository;
        }
        public async Task<CreateEntityTagCommandResponse> Handle(CreateEntityTagCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEntityTagCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new CreateEntityTagCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            Result<EntityTag> entityTag;
            switch (request.EntityType)
            {
                case EntityType.Language:
                    entityTag = EntityTag.CreateForLanguage(request.EntityId, request.TagId, request.EntityType);
                    break;
                case EntityType.LanguageLevel:
                    entityTag = EntityTag.CreateForLanguageLevel(request.EntityId, request.TagId, request.EntityType);
                    break;
                case EntityType.Chapter:
                    entityTag = EntityTag.CreateForChapter(request.EntityId, request.TagId, request.EntityType);
                    break;
                case EntityType.LanguageCompetence:
                    entityTag = EntityTag.CreateForLanguageCompetence(request.EntityId, request.TagId, request.EntityType);
                    break;
                case EntityType.Lesson:
                    entityTag = EntityTag.CreateForLesson(request.EntityId, request.TagId, request.EntityType);
                    break;
                default:
                    return new CreateEntityTagCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { "Invalid entity type." }
                    };
            }

            if (!entityTag.IsSuccess)
            {
                return new CreateEntityTagCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { entityTag.Error }
                };
            }

            await entityTagRepository.AddAsync(entityTag.Value);

            return new CreateEntityTagCommandResponse
            {
                Success = true
            };
        }
    }
}

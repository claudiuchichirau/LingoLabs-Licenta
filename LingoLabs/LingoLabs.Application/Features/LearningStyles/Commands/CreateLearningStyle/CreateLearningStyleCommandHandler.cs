using LingoLabs.Application.Persistence;
using LingoLabs.Domain.Entities;
using MediatR;

namespace LingoLabs.Application.Features.LearningStyles.Commands.CreateLearningStyle
{
    public class CreateLearningStyleCommandHandler: IRequestHandler<CreateLearningStyleCommand, CreateLearningStyleCommandResponse>
    {
        private readonly ILearningStyleRepository repository;

        public CreateLearningStyleCommandHandler(ILearningStyleRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateLearningStyleCommandResponse> Handle(CreateLearningStyleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLearningStyleCommandValidator(repository);
            var validationResult = await validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                return new CreateLearningStyleCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var learningStyle = LearningStyle.Create(request.LearningStyleName, request.LearningType);
            if(learningStyle.IsSuccess)
            {
                await repository.AddAsync(learningStyle.Value);
                return new CreateLearningStyleCommandResponse()
                {
                    LearningStyle = new CreateLearningStyleDto
                    {
                        LearningStyleId = learningStyle.Value.LearningStyleId,
                        LearningStyleName = learningStyle.Value.LearningStyleName,
                        LearningType = learningStyle.Value.LearningType
                    }
                };
            }

            return new CreateLearningStyleCommandResponse()
            {
                Success = false,
                ValidationsErrors = new List<string> { learningStyle.Error }
            };
        }
    }
}

using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler: IRequestHandler<CreateQuestionCommand, CreateQuestionCommandResponse>
    {
        private readonly IQuestionRepository repository;

        public CreateQuestionCommandHandler(IQuestionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateQuestionCommandResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateQuestionCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateQuestionCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var question = Question.Create(request.QuestionRequirement, request.QuestionLearningType, request.LessonId);
            if (question.IsSuccess)
            {
                await repository.AddAsync(question.Value);
                return new CreateQuestionCommandResponse
                {
                    Question = new CreateQuestionDto
                    {
                        QuestionId = question.Value.QuestionId,
                        QuestionRequirement = question.Value.QuestionRequirement,
                        QuestionLearningType = question.Value.QuestionLearningType,
                        LessonId = question.Value.LessonId
                    }
                };
            }

            return new CreateQuestionCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { question.Error }
            };
        }
    }
}

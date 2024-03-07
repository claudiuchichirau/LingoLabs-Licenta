using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, DeleteQuestionCommandResponse>
    {
        private readonly IQuestionRepository questionRepository;

        public DeleteQuestionCommandHandler(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }
        public async Task<DeleteQuestionCommandResponse> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteQuestionCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) 
            {
                return new DeleteQuestionCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var question = await questionRepository.FindByIdAsync(request.QuestionId);
            if(!question.IsSuccess)
            {
                return new DeleteQuestionCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { question.Error }
                };
            }

            await questionRepository.DeleteAsync(request.QuestionId);

            return new DeleteQuestionCommandResponse
            {
                Success = true
            };
        }
    }
}

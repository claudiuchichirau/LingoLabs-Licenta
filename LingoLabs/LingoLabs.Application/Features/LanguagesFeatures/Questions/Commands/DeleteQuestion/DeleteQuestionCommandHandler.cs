using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.DeleteLanguageCompetenceResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.DeleteLessonResult;
using LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.DeleteChoice;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, DeleteQuestionCommandResponse>
    {
        private readonly IQuestionRepository questionRepository;
        private readonly DeleteChoiceCommandHandler deleteChoiceCommandHandler;

        public DeleteQuestionCommandHandler(IQuestionRepository questionRepository, DeleteChoiceCommandHandler deleteChoiceCommandHandler)
        {
            this.questionRepository = questionRepository;
            this.deleteChoiceCommandHandler = deleteChoiceCommandHandler;
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

            var choices = question.Value.Choices.ToList();

            foreach (Choice choice in choices)
            {
                var deleteChoiceCommand = new DeleteChoiceCommand { ChoiceId = choice.ChoiceId };
                var deleteChoiceCommandResponse = await deleteChoiceCommandHandler.Handle(deleteChoiceCommand, cancellationToken);

                if (!deleteChoiceCommandResponse.Success)
                {
                    return new DeleteQuestionCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteChoiceCommandResponse.ValidationsErrors
                    };
                }
            }

            await questionRepository.DeleteAsync(request.QuestionId);

            return new DeleteQuestionCommandResponse
            {
                Success = true
            };
        }
    }
}

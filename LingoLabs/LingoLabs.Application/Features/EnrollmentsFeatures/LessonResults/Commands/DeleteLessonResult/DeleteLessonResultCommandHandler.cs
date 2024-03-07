using LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.DeleteQuestionResult;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.DeleteLessonResult
{
    public class DeleteLessonResultCommandHandler: IRequestHandler<DeleteLessonResultCommand, DeleteLessonResultCommandResponse>
    {
        private readonly ILessonResultRepository lessonResultRepository;
        private readonly IQuestionResultRepository questionResultRepository;
        private readonly DeleteQuestionResultCommandHandler deleteQuestionResultCommandHandler;

        public DeleteLessonResultCommandHandler(ILessonResultRepository lessonResultRepository, IQuestionResultRepository questionResultRepository, DeleteQuestionResultCommandHandler deleteQuestionResultCommandHandler)
        {
            this.lessonResultRepository = lessonResultRepository;
            this.questionResultRepository = questionResultRepository;
            this.deleteQuestionResultCommandHandler = deleteQuestionResultCommandHandler;
        }

        public async Task<DeleteLessonResultCommandResponse> Handle(DeleteLessonResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteLessonResultCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new DeleteLessonResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var lessonResult = await lessonResultRepository.FindByIdAsync(request.LessonResultId);

            if (!lessonResult.IsSuccess)
            {
                return new DeleteLessonResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { lessonResult.Error }
                };
            }

            var questionResults = lessonResult.Value.QuestionResults.ToList();

            foreach (QuestionResult questionResult in questionResults)
            {
                var deleteQuestionResultCommand = new DeleteQuestionResultCommand { QuestionResultId = questionResult.QuestionResultId };
                var deleteQuestionResultCommandResponse = await deleteQuestionResultCommandHandler.Handle(deleteQuestionResultCommand, cancellationToken);

                if (!deleteQuestionResultCommandResponse.Success)
                {
                    return new DeleteLessonResultCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteQuestionResultCommandResponse.ValidationsErrors
                    };
                }
            }

            await lessonResultRepository.DeleteAsync(request.LessonResultId);

            return new DeleteLessonResultCommandResponse
            {
                Success = true
            };
        }
    }
}

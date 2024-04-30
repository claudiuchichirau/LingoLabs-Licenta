using LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.UpdateQuestionResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Queries;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.UpdateLessonResult
{
    public class UpdateLessonResultCommandHandler : IRequestHandler<UpdateLessonResultCommand, UpdateLessonResultCommandResponse>
    {
        private readonly ILessonResultRepository lessonResultRepository;
        private readonly UpdateQuestionResultCommandHandler updateQuestionResultCommandHandler;

        public UpdateLessonResultCommandHandler(ILessonResultRepository lessonResultRepository, UpdateQuestionResultCommandHandler updateQuestionResultCommandHandler)
        {
            this.lessonResultRepository = lessonResultRepository;
            this.updateQuestionResultCommandHandler = updateQuestionResultCommandHandler;
        }
        public async Task<UpdateLessonResultCommandResponse> Handle(UpdateLessonResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLessonResultCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new UpdateLessonResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var lessonResult = await lessonResultRepository.FindByIdAsync(request.LessonResultId);

            if(!lessonResult.IsSuccess)
            {
                return new UpdateLessonResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { lessonResult.Error }
                };
            }


            var questionResults = request.QuestionResults.Select(q => new QuestionResultDto
            {
                QuestionResultId = q.QuestionResultId,
                QuestionId = q.QuestionId,
                LessonResultId = q.LessonResultId,
                IsCorrect = q.IsCorrect
            }).ToList();

            foreach (var questionResult in request.QuestionResults)
            {
                var updateQuestionResultCommand = new UpdateQuestionResultCommand
                {
                    QuestionResultId = questionResult.QuestionResultId,
                    IsCorrect = questionResult.IsCorrect
                };

                var updateQuestionResultCommandResponse = await updateQuestionResultCommandHandler.Handle(updateQuestionResultCommand, cancellationToken);

                if (!updateQuestionResultCommandResponse.Success)
                {
                    return new UpdateLessonResultCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = updateQuestionResultCommandResponse.ValidationsErrors
                    };
                }
            }

            lessonResult.Value.UpdateLessonResult(request.IsCompleted);

            await lessonResultRepository.UpdateAsync(lessonResult.Value);

            return new UpdateLessonResultCommandResponse
            {
                Success = true,
                UpdateLessonResult = new UpdateLessonResultDto
                {
                    IsCompleted = lessonResult.Value.IsCompleted,
                    QuestionResults = lessonResult.Value.QuestionResults.Select(q => new QuestionResults.Queries.QuestionResultDto
                    {
                        QuestionResultId = q.QuestionResultId,
                        QuestionId = q.QuestionId,
                        LessonResultId = q.LessonResultId,
                        IsCorrect = q.IsCorrect,
                    }).ToList()
                }
            };
        }
    }
}

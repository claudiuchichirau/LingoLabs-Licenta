using LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.UpdateChapterResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.UpdateQuestionResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Queries;
using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.UpdateLessonResult
{
    public class UpdateLessonResultCommandHandler : IRequestHandler<UpdateLessonResultCommand, UpdateLessonResultCommandResponse>
    {
        private readonly ILessonResultRepository lessonResultRepository;
        private readonly IChapterResultRepository chapterResultRepository;
        private readonly UpdateQuestionResultCommandHandler updateQuestionResultCommandHandler;
        private readonly UpdateChapterResultCommandHandler updateChapterResultCommandHandler;

        public UpdateLessonResultCommandHandler(ILessonResultRepository lessonResultRepository, IChapterResultRepository chapterResultRepository, UpdateQuestionResultCommandHandler updateQuestionResultCommandHandler, UpdateChapterResultCommandHandler updateChapterResultCommandHandler)
        {
            this.lessonResultRepository = lessonResultRepository;
            this.chapterResultRepository = chapterResultRepository;
            this.updateQuestionResultCommandHandler = updateQuestionResultCommandHandler;
            this.updateChapterResultCommandHandler = updateChapterResultCommandHandler;
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

            var chapterResult = await chapterResultRepository.FindByIdAsync(lessonResult.Value.ChapterResultId);

            if (!chapterResult.IsSuccess)
            {
                return new UpdateLessonResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapterResult.Error }
                };
            }

            bool IsChapteResultCompleted = true;
            foreach (var chapterLessonResult in chapterResult.Value.LessonResults)
            {
                if (!chapterLessonResult.IsCompleted)
                {
                    IsChapteResultCompleted = false;
                }
            }

            if (IsChapteResultCompleted)
            {
                var updateChapterResultCommand = new UpdateChapterResultCommand
                {
                    ChapterResultId = chapterResult.Value.ChapterResultId,
                    IsCompleted = true
                };

                var updateChapterResultCommandResponse = await updateChapterResultCommandHandler.Handle(updateChapterResultCommand, cancellationToken);

                if (!updateChapterResultCommandResponse.Success)
                {
                    return new UpdateLessonResultCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = updateChapterResultCommandResponse.ValidationsErrors
                    };
                }
            }

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

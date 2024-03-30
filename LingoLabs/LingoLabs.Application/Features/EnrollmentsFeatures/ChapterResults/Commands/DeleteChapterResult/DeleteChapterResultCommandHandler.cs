using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.DeleteLanguageCompetenceResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.DeleteLessonResult;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.DeleteChapterResult
{
    public class DeleteChapterResultCommandHandler : IRequestHandler<DeleteChapterResultCommand, DeleteChapterResultCommandResponse>
    {
        private readonly IChapterResultRepository chapterResultRepository;
        private readonly DeleteLessonResultCommandHandler deleteLessonResultCommandHandler;

        public DeleteChapterResultCommandHandler(IChapterResultRepository chapterResultRepository, DeleteLessonResultCommandHandler deleteLanguageCompetenceResultCommandHandler)
        {
            this.chapterResultRepository = chapterResultRepository;
            this.deleteLessonResultCommandHandler = deleteLessonResultCommandHandler;
        }

        public async Task<DeleteChapterResultCommandResponse> Handle(DeleteChapterResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteChapterResultCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new DeleteChapterResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var chapterResult = await chapterResultRepository.FindByIdAsync(request.ChapterResultId);
            if(!chapterResult.IsSuccess)
            {
                return new DeleteChapterResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapterResult.Error }
                };
            }

            var lessonResults = chapterResult.Value.LessonResults.ToList();

            foreach (LessonResult lessonResult in lessonResults) 
            {
                var deleteLessonResultCommand = new DeleteLessonResultCommand { LessonResultId = lessonResult.LessonResultId };
                var deleteLessonResultCommandResponse = await deleteLessonResultCommandHandler.Handle(deleteLessonResultCommand, cancellationToken);

                if(!deleteLessonResultCommandResponse.Success)
                {
                    return new DeleteChapterResultCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteLessonResultCommandResponse.ValidationsErrors
                    };
                }
            }

            await chapterResultRepository.DeleteAsync(request.ChapterResultId);

            return new DeleteChapterResultCommandResponse
            {
                Success = true
            };
        }
    }
}

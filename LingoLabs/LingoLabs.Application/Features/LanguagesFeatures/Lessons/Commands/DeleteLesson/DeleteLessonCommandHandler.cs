using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.DeleteLessonResult;
using LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.DeleteEntityTag;
using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.DeleteLanguageCompetence;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, DeleteLessonCommandResponse>
    {
        private readonly ILessonRepository lessonRepository;
        private readonly ILessonResultRepository lessonResultRepository;
        private readonly DeleteEntityTagCommandHandler deleteEntityTagCommandHandler;
        private readonly DeleteLessonResultCommandHandler deleteLessonResultCommandHandler;

        public DeleteLessonCommandHandler(ILessonRepository lessonRepository, DeleteEntityTagCommandHandler deleteEntityTagCommandHandler, ILessonResultRepository lessonResultRepository, DeleteLessonResultCommandHandler deleteLessonResultCommandHandler)
        {
            this.lessonRepository = lessonRepository;
            this.lessonResultRepository = lessonResultRepository;
            this.deleteEntityTagCommandHandler = deleteEntityTagCommandHandler;
            this.deleteLessonResultCommandHandler = deleteLessonResultCommandHandler;
        }
        public async Task<DeleteLessonCommandResponse> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteLessonCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new DeleteLessonCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var lesson = await lessonRepository.FindByIdAsync(request.LessonId);

            if(!lesson.IsSuccess)
            {
                return new DeleteLessonCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { lesson.Error }
                };
            }

            var entityTags = lesson.Value.LessonTags.ToList();

            foreach (EntityTag entityTag in entityTags)
            {
                var deleteEntityTagCommand = new DeleteEntityTagCommand { EntityTagId = entityTag.EntityTagId };
                var deleteEntityTagCommandResponse = await deleteEntityTagCommandHandler.Handle(deleteEntityTagCommand, cancellationToken);

                if (!deleteEntityTagCommandResponse.Success)
                {
                    return new DeleteLessonCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteEntityTagCommandResponse.ValidationsErrors
                    };
                }
            }

            var lessonResults = await lessonResultRepository.GetLessonResultsByLessonId(request.LessonId);

            foreach (var lessonResult in lessonResults)
            {
                var deleteLessonResultCommand = new DeleteLessonResultCommand { LessonResultId = lessonResult.LessonResultId };
                var deleteLessonResultCommandResponse = await deleteLessonResultCommandHandler.Handle(deleteLessonResultCommand, cancellationToken);

                if (!deleteLessonResultCommandResponse.Success)
                {
                    return new DeleteLessonCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteLessonResultCommandResponse.ValidationsErrors
                    };
                }
            }

            await lessonRepository.DeleteAsync(request.LessonId);

            return new DeleteLessonCommandResponse
            {
                Success = true
            };
        }
    }
}

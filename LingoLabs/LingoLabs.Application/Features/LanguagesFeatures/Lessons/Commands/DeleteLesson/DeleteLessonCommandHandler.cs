using LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.DeleteEntityTag;
using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.DeleteLanguageCompetence;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, DeleteLessonCommandResponse>
    {
        private readonly ILessonRepository lessonRepository;
        private readonly DeleteEntityTagCommandHandler deleteEntityTagCommandHandler;

        public DeleteLessonCommandHandler(ILessonRepository lessonRepository, DeleteEntityTagCommandHandler deleteEntityTagCommandHandler)
        {
            this.lessonRepository = lessonRepository;
            this.deleteEntityTagCommandHandler = deleteEntityTagCommandHandler;
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

            await lessonRepository.DeleteAsync(request.LessonId);

            return new DeleteLessonCommandResponse
            {
                Success = true
            };
        }
    }
}

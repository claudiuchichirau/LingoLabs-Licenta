using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, DeleteLessonCommandResponse>
    {
        private readonly ILessonRepository lessonRepository;

        public DeleteLessonCommandHandler(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;
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

            await lessonRepository.DeleteAsync(request.LessonId);

            return new DeleteLessonCommandResponse
            {
                Success = true
            };
        }
    }
}

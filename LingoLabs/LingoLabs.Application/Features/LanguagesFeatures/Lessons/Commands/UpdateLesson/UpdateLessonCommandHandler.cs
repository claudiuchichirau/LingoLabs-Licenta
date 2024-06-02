using LingoLabs.Application.Persistence.Languages;
using MediatR;
using System.Web;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, UpdateLessonCommandResponse>
    {
        private readonly ILessonRepository lessonRepository;

        public UpdateLessonCommandHandler(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }
        public async Task<UpdateLessonCommandResponse> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLessonCommandValidator(lessonRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new UpdateLessonCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var lesson = await lessonRepository.FindByIdAsync(request.LessonId);

            if(!lesson.IsSuccess)
            {
                return new UpdateLessonCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { lesson.Error }
                };
            }

            string newVideoLink = null;

            if (!string.IsNullOrEmpty(request.LessonVideoLink))
            {
                string video = request.LessonVideoLink;
                string videoId = HttpUtility.ParseQueryString(new Uri(request.LessonVideoLink).Query).Get("v");

                // Construct the new URL
                newVideoLink = $"https://www.youtube.com/embed/{videoId}";
            }

            lesson.Value.UpdateLesson(
                request.LessonTitle,
                request.LessonDescription,
                request.LessonRequirement,
                request.LessonContent,
                request.LessonImageData,
                newVideoLink,
                request.LessonPriorityNumber);

            await lessonRepository.UpdateAsync(lesson.Value);

            return new UpdateLessonCommandResponse
            {
                Success = true,
                UpdateLesson = new UpdateLessonDto
                {
                    LessonTitle = request.LessonTitle,
                    LessonDescription = request.LessonDescription,
                    LessonRequirement = request.LessonRequirement,
                    LessonContent = request.LessonContent,
                    LessonImageData = request.LessonImageData,
                    LessonVideoLink = newVideoLink,
                    LessonPriorityNumber = request.LessonPriorityNumber
                }
            };
        }
    }
}

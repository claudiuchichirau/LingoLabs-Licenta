using LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.UpdateLanguageLevel;
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
            var updateLessonDto = request.UpdateLessonDto;

            var videoId = HttpUtility.ParseQueryString(new Uri(updateLessonDto.LessonVideoLink).Query).Get("v");

            // Construct the new URL
            var newVideoLink = $"https://www.youtube.com/embed/{videoId}";

            lesson.Value.UpdateLesson(
                updateLessonDto.LessonTitle,
                updateLessonDto.LessonDescription,
                updateLessonDto.LessonRequirement,
                updateLessonDto.LessonContent,
                updateLessonDto.LessonImageData,
                newVideoLink,
                updateLessonDto.LessonPriorityNumber);

            await lessonRepository.UpdateAsync(lesson.Value);

            return new UpdateLessonCommandResponse
            {
                Success = true,
                UpdateLesson = new UpdateLessonDto
                {
                    LessonTitle = updateLessonDto.LessonTitle,
                    LessonDescription = updateLessonDto.LessonDescription,
                    LessonRequirement = updateLessonDto.LessonRequirement,
                    LessonContent = updateLessonDto.LessonContent,
                    LessonImageData = updateLessonDto.LessonImageData,
                    LessonVideoLink = updateLessonDto.LessonVideoLink,
                    LessonPriorityNumber = updateLessonDto.LessonPriorityNumber
                }
            };
        }
    }
}

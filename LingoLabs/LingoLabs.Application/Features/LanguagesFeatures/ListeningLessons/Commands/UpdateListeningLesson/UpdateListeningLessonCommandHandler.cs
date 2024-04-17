using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateLesson;
using LingoLabs.Application.Persistence.Languages;
using MediatR;
using System.Web;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.UpdateListeningLesson
{
    public class UpdateListeningLessonCommandHandler : IRequestHandler<UpdateListeningLessonCommand, UpdateListeningLessonCommandResponse>
    {
        private readonly IListeningLessonRepository listeningLessonRepository;
        private readonly ILessonRepository lessonRepository;

        public UpdateListeningLessonCommandHandler(IListeningLessonRepository listeningLessonRepository, ILessonRepository lessonRepository)
        {
            this.listeningLessonRepository = listeningLessonRepository;
            this.lessonRepository = lessonRepository;
        }
        public async Task<UpdateListeningLessonCommandResponse> Handle(UpdateListeningLessonCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateListeningLessonCommandValidator(lessonRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid) 
            {
                return new UpdateListeningLessonCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var listeningLesson = await listeningLessonRepository.FindByIdAsync(request.LessonId);

            if(!listeningLesson.IsSuccess)
            {
                return new UpdateListeningLessonCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { listeningLesson.Error }
                };
            }

            string newVideoLink = null;

            if (!string.IsNullOrEmpty(request.LessonVideoLink))
            {
                string videoId = HttpUtility.ParseQueryString(new Uri(request.LessonVideoLink).Query).Get("v");

                // Construct the new URL
                newVideoLink = $"https://www.youtube.com/embed/{videoId}";
            }

            listeningLesson.Value.UpdateListeningLanguage(
                request.LessonTitle,
                request.LessonDescription,
                request.LessonRequirement,
                request.LessonContent,
                request.LessonImageData,
                newVideoLink,
                request.TextScript,
                request.Accents,
                request.LessonPriorityNumber);

            await listeningLessonRepository.UpdateAsync(listeningLesson.Value);

            return new UpdateListeningLessonCommandResponse
            {
                Success = true,
                UpdateListeningLesson = new UpdateListeningLessonDto
                {
                    LessonTitle = request.LessonTitle,
                    LessonDescription = request.LessonDescription,
                    LessonRequirement = request.LessonRequirement,
                    LessonContent = request.LessonContent,
                    LessonImageData = request.LessonImageData,
                    LessonVideoLink = newVideoLink,
                    LessonPriorityNumber = request.LessonPriorityNumber,
                    TextScript = request.TextScript,
                    Accents = request.Accents
                }
            };
        }
    }
}

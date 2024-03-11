using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.UpdateListeningLesson
{
    public class UpdateListeningLessonCommandHandler : IRequestHandler<UpdateListeningLessonCommand, UpdateListeningLessonCommandResponse>
    {
        private readonly IListeningLessonRepository listeningLessonRepository;

        public UpdateListeningLessonCommandHandler(IListeningLessonRepository listeningLessonRepository)
        {
            this.listeningLessonRepository = listeningLessonRepository;
        }
        public async Task<UpdateListeningLessonCommandResponse> Handle(UpdateListeningLessonCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateListeningLessonCommandValidator();
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

            var updateListeningLessonDto = request.UpdateListeningLessonDto;

            listeningLesson.Value.UpdateListeningLanguage(
                updateListeningLessonDto.LessonTitle,
                updateListeningLessonDto.LessonDescription,
                updateListeningLessonDto.LessonRequirement,
                updateListeningLessonDto.LessonContent,
                updateListeningLessonDto.LessonImageData,
                updateListeningLessonDto.LessonVideoLink,
                updateListeningLessonDto.TextScript,
                updateListeningLessonDto.Accents);

            await listeningLessonRepository.UpdateAsync(listeningLesson.Value);

            return new UpdateListeningLessonCommandResponse
            {
                Success = true,
                UpdateListeningLesson = new UpdateListeningLessonDto
                {
                    LessonTitle = updateListeningLessonDto.LessonTitle,
                    LessonDescription = updateListeningLessonDto.LessonDescription,
                    LessonRequirement = updateListeningLessonDto.LessonRequirement,
                    LessonContent = updateListeningLessonDto.LessonContent,
                    LessonImageData = updateListeningLessonDto.LessonImageData,
                    LessonVideoLink = updateListeningLessonDto.LessonVideoLink,
                    TextScript = updateListeningLessonDto.TextScript,
                    Accents = updateListeningLessonDto.Accents
                }
            };
        }
    }
}

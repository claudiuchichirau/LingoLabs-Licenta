using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.CreateListeningLesson
{
    public class CreateListeningLessonCommandHandler: IRequestHandler<CreateListeningLessonCommand, CreateListeningLessonCommandResponse>
    {
        private readonly IListeningLessonRepository repository;

        public CreateListeningLessonCommandHandler(IListeningLessonRepository repository)
        {
            this.repository = repository;
        }
        
        public async Task<CreateListeningLessonCommandResponse> Handle(CreateListeningLessonCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateListeningLessonCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateListeningLessonCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var listeningLesson = ListeningLesson.Create(request.LessonTitle, request.LessonType, request.AudioContents, request.Accents);
            if (listeningLesson.IsSuccess)
            {
                await repository.AddAsync(listeningLesson.Value);
                return new CreateListeningLessonCommandResponse
                {
                    ListeningLesson = new CreateListeningLessonDto
                    {
                        LessonId = listeningLesson.Value.LessonId,
                        LessonTitle = listeningLesson.Value.LessonTitle,
                        LessonType = listeningLesson.Value.LessonType,
                        AudioContents = listeningLesson.Value.AudioContents,
                        Accents = listeningLesson.Value.Accents
                    }
                };
            }

            return new CreateListeningLessonCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { listeningLesson.Error }
            };
        }
    }
}

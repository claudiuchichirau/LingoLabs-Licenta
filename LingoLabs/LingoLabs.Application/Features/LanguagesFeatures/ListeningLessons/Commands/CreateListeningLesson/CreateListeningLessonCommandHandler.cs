using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.CreateListeningLesson
{
    public class CreateListeningLessonCommandHandler: IRequestHandler<CreateListeningLessonCommand, CreateListeningLessonCommandResponse>
    {
        private readonly IListeningLessonRepository repository;
        private readonly ILanguageCompetenceRepository _languageCompetenceRepository;

        public CreateListeningLessonCommandHandler(IListeningLessonRepository repository, ILanguageCompetenceRepository _languageCompetenceRepository)
        {
            this.repository = repository;
            this._languageCompetenceRepository = _languageCompetenceRepository;
        }
        
        public async Task<CreateListeningLessonCommandResponse> Handle(CreateListeningLessonCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateListeningLessonCommandValidator(_languageCompetenceRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateListeningLessonCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var listeningLesson = ListeningLesson.Create(request.LessonTitle, request.LessonType, request.ChapterId, request.TextScript, request.Accents);
            if (listeningLesson.IsSuccess)
            {
                await repository.AddAsync(listeningLesson.Value);
                return new CreateListeningLessonCommandResponse
                {
                    ListeningLesson = new CreateListeningLessonDto
                    {
                        LessonId = listeningLesson.Value.LessonId,
                        LessonTitle = listeningLesson.Value.LessonTitle,
                        ChapterId = listeningLesson.Value.ChapterId,
                        LessonType = listeningLesson.Value.LessonType,
                        TextScript = listeningLesson.Value.TextScript,
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

using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandHandler: IRequestHandler<CreateLessonCommand, CreateLessonCommandResponse>
    {
        private readonly ILessonRepository repository;
        private readonly ILanguageCompetenceRepository _languageCompetenceRepository;

        public CreateLessonCommandHandler(ILessonRepository repository, ILanguageCompetenceRepository languageCompetenceRepository)
        {
            this.repository = repository;
            _languageCompetenceRepository = languageCompetenceRepository;
        }

        public async Task<CreateLessonCommandResponse> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLessonCommandValidator(_languageCompetenceRepository, repository);
            var validationResult = await validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                return new CreateLessonCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var lesson = Lesson.Create(request.LessonTitle, request.ChapterId, request.LanguageCompetenceId);
            if(lesson.IsSuccess)
            {
                await repository.AddAsync(lesson.Value);
                return new CreateLessonCommandResponse
                {
                    Lesson = new CreateLessonDto
                    {
                        LessonId = lesson.Value.LessonId,
                        LessonTitle = lesson.Value.LessonTitle,
                        ChapterId = lesson.Value.ChapterId,
                        LanguageCompetenceId = lesson.Value.LanguageCompetenceId
                    }
                };
            }

            return new CreateLessonCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { lesson.Error }
            };
        }
    }
}

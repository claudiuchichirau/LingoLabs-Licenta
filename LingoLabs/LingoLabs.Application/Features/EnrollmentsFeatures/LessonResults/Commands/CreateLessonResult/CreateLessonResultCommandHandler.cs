using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.CreateLessonResult
{
    public class CreateLessonResultCommandHandler: IRequestHandler<CreateLessonResultCommand, CreateLessonResultCommandResponse>
    {
        private readonly ILessonResultRepository repository;

        public CreateLessonResultCommandHandler(ILessonResultRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateLessonResultCommandResponse> Handle(CreateLessonResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLessonResultCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if(!validationResult.IsValid)
            {
                return new CreateLessonResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var lessonResult = LessonResult.Create(request.LessonId, request.ChapterResultId, request.LanguageCompetenceResultId, request.IsCompleted);
            if(lessonResult.IsSuccess)
            {
                await repository.AddAsync(lessonResult.Value);
                return new CreateLessonResultCommandResponse
                {
                    LessonResult = new CreateLessonResultDto
                    {
                        LessonResultId = lessonResult.Value.LessonResultId,
                        LessonId = lessonResult.Value.LessonId,
                        ChapterResultId = lessonResult.Value.ChapterResultId,
                        LanguageCompetenceResultId = lessonResult.Value.LanguageCompetenceResultId,
                        IsCompleted = false
                    }
                };
            }

            return new CreateLessonResultCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { lessonResult.Error }
            };
        }
    }
}

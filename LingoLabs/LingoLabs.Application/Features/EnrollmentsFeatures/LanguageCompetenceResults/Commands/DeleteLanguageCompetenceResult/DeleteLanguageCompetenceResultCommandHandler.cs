using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.DeleteLessonResult;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.DeleteLanguageCompetenceResult
{
    public class DeleteLanguageCompetenceResultCommandHandler : IRequestHandler<DeleteLanguageCompetenceResultCommand, DeleteLanguageCompetenceResultCommandResponse>
    {
        private readonly ILanguageCompetenceResultRepository languageCompetenceResultRepository;
        private readonly DeleteLessonResultCommandHandler deleteLessonResultCommandHandler;

        public DeleteLanguageCompetenceResultCommandHandler(ILanguageCompetenceResultRepository languageCompetenceResultRepository, DeleteLessonResultCommandHandler deleteLessonResultCommandHandler)
        {
            this.languageCompetenceResultRepository = languageCompetenceResultRepository;
            this.deleteLessonResultCommandHandler = deleteLessonResultCommandHandler;
        }
        public async Task<DeleteLanguageCompetenceResultCommandResponse> Handle(DeleteLanguageCompetenceResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteLanguageCompetenceResultComandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new DeleteLanguageCompetenceResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var languageCompetenceResult = await languageCompetenceResultRepository.FindByIdAsync(request.LanguageCompetenceResultId);

            if(!languageCompetenceResult.IsSuccess)
            {
                return new DeleteLanguageCompetenceResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { languageCompetenceResult.Error }
                };
            }

            var lessonResults = languageCompetenceResult.Value.LessonsResults.ToList();

            foreach(LessonResult lessonResult in lessonResults)
            {
                var deleteLessonResultCommand = new DeleteLessonResultCommand { LessonResultId = lessonResult.LessonResultId };
                var deleteLessonResultCommandResponse = await deleteLessonResultCommandHandler.Handle(deleteLessonResultCommand, cancellationToken);

                if(!deleteLessonResultCommandResponse.Success)
                {
                    return new DeleteLanguageCompetenceResultCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteLessonResultCommandResponse.ValidationsErrors
                    };
                }
            }

            await languageCompetenceResultRepository.DeleteAsync(request.LanguageCompetenceResultId);

            return new DeleteLanguageCompetenceResultCommandResponse
            {
                Success = true
            };

        }
    }
}

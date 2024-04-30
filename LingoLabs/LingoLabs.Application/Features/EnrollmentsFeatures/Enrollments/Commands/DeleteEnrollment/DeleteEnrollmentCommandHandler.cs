using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.DeleteLanguageCompetenceResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.DeleteLanguageLevelResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.DeleteUserLanguageLevel;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Commands.DeleteEnrollment
{
    public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, DeleteEnrollmentCommandResponse>
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly DeleteLanguageLevelResultCommandHandler deleteLanguageLevelResultCommandHandler;
        private readonly DeleteUserLanguageLevelCommandHandler deleteUserLanguageLevelCommandHandler;
        private readonly DeleteLanguageCompetenceResultCommandHandler deleteLanguageCompetenceResultCommandHandler;

        public DeleteEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, DeleteLanguageLevelResultCommandHandler deleteLanguageLevelResultCommandHandler, DeleteUserLanguageLevelCommandHandler deleteUserLanguageLevelCommandHandler, DeleteLanguageCompetenceResultCommandHandler deleteLanguageCompetenceResultCommandHandler)
        {
            this.enrollmentRepository = enrollmentRepository;
            this.deleteLanguageLevelResultCommandHandler = deleteLanguageLevelResultCommandHandler;
            this.deleteUserLanguageLevelCommandHandler = deleteUserLanguageLevelCommandHandler;
            this.deleteLanguageCompetenceResultCommandHandler = deleteLanguageCompetenceResultCommandHandler;
        }

        public async Task<DeleteEnrollmentCommandResponse> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteEnrollmentCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new DeleteEnrollmentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var enrollment = await enrollmentRepository.FindByIdAsync(request.EnrollmentId);

            if(!enrollment.IsSuccess)
            {
                return new DeleteEnrollmentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { enrollment.Error }
                };
            }

            var userLanguageLevels = enrollment.Value.UserLanguageLevels.ToList();

            foreach (UserLanguageLevel userLanguageLevel in userLanguageLevels)
            {
                var deleteUserLanguageLevelCommand = new DeleteUserLanguageLevelCommand { UserLanguageLevelId = userLanguageLevel.UserLanguageLevelId };
                var deleteUserLanguageLevelCommandResponse = await deleteUserLanguageLevelCommandHandler.Handle(deleteUserLanguageLevelCommand, cancellationToken);

                if(!deleteUserLanguageLevelCommandResponse.Success)
                {
                    return new DeleteEnrollmentCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteUserLanguageLevelCommandResponse.ValidationsErrors
                    };
                }   
            }

            var languageLevelResults = enrollment.Value.LanguageLevelResults.ToList();

            foreach (LanguageLevelResult languageLevelResult in languageLevelResults)
            {
                var deleteLanguageLevelResultCommand = new DeleteLanguageLevelResultCommand { LanguageLevelResultId = languageLevelResult.LanguageLevelResultId };
                var deleteLanguageLevelResultCommandResponse = await deleteLanguageLevelResultCommandHandler.Handle(deleteLanguageLevelResultCommand, cancellationToken);

                if(!deleteLanguageLevelResultCommandResponse.Success)
                {
                    return new DeleteEnrollmentCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteLanguageLevelResultCommandResponse.ValidationsErrors
                    };
                }   
            }

            var languageCompetenceResults = enrollment.Value.LanguageCompetenceResults.ToList();

            foreach (LanguageCompetenceResult languageCompetenceResult in languageCompetenceResults)
            {
                var deleteLanguageCompetenceResultCommand = new DeleteLanguageCompetenceResultCommand { LanguageCompetenceResultId = languageCompetenceResult.LanguageCompetenceResultId };
                var deleteLanguageCompetenceResultCommandResponse = await deleteLanguageCompetenceResultCommandHandler.Handle(deleteLanguageCompetenceResultCommand, cancellationToken);

                if (!deleteLanguageCompetenceResultCommandResponse.Success)
                {
                    return new DeleteEnrollmentCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteLanguageCompetenceResultCommandResponse.ValidationsErrors
                    };
                }
            }

            await enrollmentRepository.DeleteAsync(request.EnrollmentId);

            return new DeleteEnrollmentCommandResponse
            {
                Success = true
            };
        }
    }
}

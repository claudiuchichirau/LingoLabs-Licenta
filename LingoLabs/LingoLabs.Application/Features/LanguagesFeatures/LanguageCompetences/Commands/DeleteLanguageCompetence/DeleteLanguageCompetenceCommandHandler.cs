using LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.DeleteUserLanguageLevel;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.DeleteLanguageCompetence
{
    public class DeleteLanguageCompetenceCommandHandler: IRequestHandler<DeleteLanguageCompetenceCommand, DeleteLanguageCompetenceCommandResponse>
    {
        private readonly ILanguageCompetenceRepository languageCompetenceRepository;
        private readonly DeleteUserLanguageLevelCommandHandler deleteUserLanguageLevelCommandHandler;

        public DeleteLanguageCompetenceCommandHandler(ILanguageCompetenceRepository languageCompetenceRepository, DeleteUserLanguageLevelCommandHandler deleteUserLanguageLevelCommandHandler)
        {
            this.languageCompetenceRepository = languageCompetenceRepository;
            this.deleteUserLanguageLevelCommandHandler = deleteUserLanguageLevelCommandHandler;
        }

        public async Task<DeleteLanguageCompetenceCommandResponse> Handle(DeleteLanguageCompetenceCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteLanguageCompetenceCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new DeleteLanguageCompetenceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var languageCompetence = await languageCompetenceRepository.FindByIdAsync(request.LanguageCompetenceId);
            if(!languageCompetence.IsSuccess)
            {
                return new DeleteLanguageCompetenceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { languageCompetence.Error }
                };
            }

            var userLanguageLevels = languageCompetence.Value.UserLanguageLevels.ToList();

            foreach (UserLanguageLevel userLanguageLevel in userLanguageLevels)
            {
                var deleteUserLanguageLevelCommand = new DeleteUserLanguageLevelCommand { UserLanguageLevelId = userLanguageLevel.UserLanguageLevelId };
                var deleteUserLanguageLevelCommandResponse = await deleteUserLanguageLevelCommandHandler.Handle(deleteUserLanguageLevelCommand, cancellationToken);

                if(!deleteUserLanguageLevelCommandResponse.Success)
                {
                    return new DeleteLanguageCompetenceCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteUserLanguageLevelCommandResponse.ValidationsErrors
                    };
                }   
            }

            await languageCompetenceRepository.DeleteAsync(request.LanguageCompetenceId);

            return new DeleteLanguageCompetenceCommandResponse
            {
                Success = true
            };
        }
    }
}

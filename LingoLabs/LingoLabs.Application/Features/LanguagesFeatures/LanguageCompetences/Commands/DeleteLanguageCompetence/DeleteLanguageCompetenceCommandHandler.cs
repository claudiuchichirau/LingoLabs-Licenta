using LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.DeleteUserLanguageLevel;
using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries.GetById;
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
        private readonly IUserLanguageLevelRepository userLanguageLevelRepository;

        public DeleteLanguageCompetenceCommandHandler(ILanguageCompetenceRepository languageCompetenceRepository, DeleteUserLanguageLevelCommandHandler deleteUserLanguageLevelCommandHandler, IUserLanguageLevelRepository userLanguageLevelRepository)
        {
            this.languageCompetenceRepository = languageCompetenceRepository;
            this.deleteUserLanguageLevelCommandHandler = deleteUserLanguageLevelCommandHandler;
            this.userLanguageLevelRepository = userLanguageLevelRepository;
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

            var userLanguageLevelsResult = await userLanguageLevelRepository.FindByLanguageCompetenceIdAsync(request.LanguageCompetenceId);

            if (!userLanguageLevelsResult.IsSuccess)
            {
                // Gestionați eroarea aici
                return new DeleteLanguageCompetenceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { userLanguageLevelsResult.Error }
                };
            }

            var userLanguageLevels = userLanguageLevelsResult.Value;

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

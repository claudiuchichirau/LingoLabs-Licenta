using LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.DeleteUserLanguageLevel;
using LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.DeleteEntityTag;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.DeleteLanguageCompetence
{
    public class DeleteLanguageCompetenceCommandHandler: IRequestHandler<DeleteLanguageCompetenceCommand, DeleteLanguageCompetenceCommandResponse>
    {
        private readonly ILanguageCompetenceRepository languageCompetenceRepository;
        private readonly DeleteUserLanguageLevelCommandHandler deleteUserLanguageLevelCommandHandler;
        private readonly IUserLanguageLevelRepository userLanguageLevelRepository;
        private readonly DeleteEntityTagCommandHandler deleteEntityTagCommandHandler;

        public DeleteLanguageCompetenceCommandHandler(ILanguageCompetenceRepository languageCompetenceRepository, DeleteUserLanguageLevelCommandHandler deleteUserLanguageLevelCommandHandler, IUserLanguageLevelRepository userLanguageLevelRepository, DeleteEntityTagCommandHandler deleteEntityTagCommandHandler)
        {
            this.languageCompetenceRepository = languageCompetenceRepository;
            this.deleteUserLanguageLevelCommandHandler = deleteUserLanguageLevelCommandHandler;
            this.userLanguageLevelRepository = userLanguageLevelRepository;
            this.deleteEntityTagCommandHandler = deleteEntityTagCommandHandler;
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

            var entityTags = languageCompetence.Value.LearningCompetenceTags.ToList();

            foreach (EntityTag entityTag in entityTags)
            {
                var deleteEntityTagCommand = new DeleteEntityTagCommand { EntityTagId = entityTag.EntityTagId };
                var deleteEntityTagCommandResponse = await deleteEntityTagCommandHandler.Handle(deleteEntityTagCommand, cancellationToken);

                if (!deleteEntityTagCommandResponse.Success)
                {
                    return new DeleteLanguageCompetenceCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteEntityTagCommandResponse.ValidationsErrors
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

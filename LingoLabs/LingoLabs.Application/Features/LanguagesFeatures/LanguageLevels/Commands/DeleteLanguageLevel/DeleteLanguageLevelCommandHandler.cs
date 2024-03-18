﻿using LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.DeleteUserLanguageLevel;
using LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.DeleteEntityTag;
using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.DeleteLanguageCompetence;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Enrollments;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.DeleteLanguageLevel
{
    public class DeleteLanguageLevelCommandHandler: IRequestHandler<DeleteLanguageLevelCommand, DeleteLanguageLevelCommandResponse>
    {
        private readonly ILanguageLevelRepository languageLevelRepository;
        private readonly DeleteUserLanguageLevelCommandHandler deleteUserLanguageLevelCommandHandler;
        private readonly IUserLanguageLevelRepository userLanguageLevelRepository;
        private readonly DeleteEntityTagCommandHandler deleteEntityTagCommandHandler;

        public DeleteLanguageLevelCommandHandler(ILanguageLevelRepository languageLevelRepository, DeleteUserLanguageLevelCommandHandler deleteUserLanguageLevelCommandHandler, IUserLanguageLevelRepository userLanguageLevelRepository, DeleteEntityTagCommandHandler deleteEntityTagCommandHandler)
        {
            this.languageLevelRepository = languageLevelRepository;
            this.deleteUserLanguageLevelCommandHandler = deleteUserLanguageLevelCommandHandler;
            this.userLanguageLevelRepository = userLanguageLevelRepository;
            this.deleteEntityTagCommandHandler = deleteEntityTagCommandHandler;
        }

        public async Task<DeleteLanguageLevelCommandResponse> Handle(DeleteLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteLanguageLevelCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new DeleteLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var languageLevel = await languageLevelRepository.FindByIdAsync(request.LanguageLevelId);

            if(!languageLevel.IsSuccess)
            {
                return new DeleteLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { languageLevel.Error }
                };
            }

            var userLanguageLevelsResult = await userLanguageLevelRepository.FindByLanguageLevelIdAsync(request.LanguageLevelId);

            if (!userLanguageLevelsResult.IsSuccess)
            {
                // Gestionați eroarea aici
                return new DeleteLanguageLevelCommandResponse
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

                if (!deleteUserLanguageLevelCommandResponse.Success)
                {
                    return new DeleteLanguageLevelCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteUserLanguageLevelCommandResponse.ValidationsErrors
                    };
                }
            }

            var entityTags = languageLevel.Value.LanguageLevelTags.ToList();

            foreach (EntityTag entityTag in entityTags)
            {
                var deleteEntityTagCommand = new DeleteEntityTagCommand { EntityTagId = entityTag.EntityTagId };
                var deleteEntityTagCommandResponse = await deleteEntityTagCommandHandler.Handle(deleteEntityTagCommand, cancellationToken);

                if (!deleteEntityTagCommandResponse.Success)
                {
                    return new DeleteLanguageLevelCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteEntityTagCommandResponse.ValidationsErrors
                    };
                }
            }

            await languageLevelRepository.DeleteAsync(request.LanguageLevelId);

            return new DeleteLanguageLevelCommandResponse
            {
                Success = true
            };
        }
    }
}

﻿using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.UpdateLanguageLevel
{
    public class UpdateLanguageLevelCommandHandler : IRequestHandler<UpdateLanguageLevelCommand, UpdateLanguageLevelCommandResponse>
    {
        private readonly ILanguageLevelRepository languageLevelRepository;

        public UpdateLanguageLevelCommandHandler(ILanguageLevelRepository languageLevelRepository)
        {
            this.languageLevelRepository = languageLevelRepository;
        }
        public async Task<UpdateLanguageLevelCommandResponse> Handle(UpdateLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLanguageLevelCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid) 
            {
                return new UpdateLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var languageLevel = await languageLevelRepository.FindByIdAsync(request.LanguageLevelId);

            if(!languageLevel.IsSuccess)
            {
                return new UpdateLanguageLevelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { languageLevel.Error }
                };
            }

            var updateLanguageLevelDto = request.UpdateLanguageLevelDto;

            languageLevel.Value.UpdateLanguageLevel(
                updateLanguageLevelDto.LanguageLevelAlias,
                updateLanguageLevelDto.LanguageLevelDescription,
                updateLanguageLevelDto.LanguageLevelVideoLink);

            await languageLevelRepository.UpdateAsync(languageLevel.Value);

            return new UpdateLanguageLevelCommandResponse
            {
                Success = true,
                UpdateLanguageLevel = new UpdateLanguageLevelDto
                {
                    LanguageLevelAlias = languageLevel.Value.LanguageLevelAlias,
                    LanguageLevelDescription = languageLevel.Value.LanguageLevelDescription,
                    LanguageLevelVideoLink = languageLevel.Value.LanguageLevelVideoLink
                }
            };
        }
    }
}

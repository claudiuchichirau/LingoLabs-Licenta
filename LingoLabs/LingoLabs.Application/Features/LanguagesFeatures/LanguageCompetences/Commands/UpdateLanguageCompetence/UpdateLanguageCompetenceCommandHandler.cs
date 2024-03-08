using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.UpdateLanguageCompetence
{
    public class UpdateLanguageCompetenceCommandHandler: IRequestHandler<UpdateLanguageCompetenceCommand, UpdateLanguageCompetenceCommandResponse>
    {
        private readonly ILanguageCompetenceRepository languageCompetenceRepository;

        public UpdateLanguageCompetenceCommandHandler(ILanguageCompetenceRepository languageCompetenceRepository)
        {
            this.languageCompetenceRepository = languageCompetenceRepository;
        }

        public async Task<UpdateLanguageCompetenceCommandResponse> Handle(UpdateLanguageCompetenceCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLanguageCompetenceCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new UpdateLanguageCompetenceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var languageCompetence = await languageCompetenceRepository.FindByIdAsync(request.LanguageCompetenceId);

            if(!languageCompetence.IsSuccess)
            {
                return new UpdateLanguageCompetenceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { languageCompetence.Error }
                };
            }

            var updateLanguageCompetenceDto = request.UpdateLanguageCompetenceDto;
            languageCompetence.Value.UpdateLanguageCompetence
            (
                updateLanguageCompetenceDto.LanguageCompetenceDescription,
                updateLanguageCompetenceDto.LanguageCompetenceVideoLink
            );

            await languageCompetenceRepository.UpdateAsync(languageCompetence.Value);

            return new UpdateLanguageCompetenceCommandResponse
            {
                Success = true,
                UpdateLanguageCompetenceDto = new UpdateLanguageCompetenceDto
                {
                    LanguageCompetenceDescription = languageCompetence.Value.LanguageCompetenceDescription,
                    LanguageCompetenceVideoLink = languageCompetence.Value.LanguageCompetenceVideoLink
                }
            };
        }
    }
}

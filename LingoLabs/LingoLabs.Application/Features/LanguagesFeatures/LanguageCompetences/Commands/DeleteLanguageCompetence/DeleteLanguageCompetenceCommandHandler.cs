using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.DeleteLanguageCompetence
{
    public class DeleteLanguageCompetenceCommandHandler: IRequestHandler<DeleteLanguageCompetenceCommand, DeleteLanguageCompetenceCommandResponse>
    {
        private readonly ILanguageCompetenceRepository languageCompetenceRepository;

        public DeleteLanguageCompetenceCommandHandler(ILanguageCompetenceRepository languageCompetenceRepository)
        {
            this.languageCompetenceRepository = languageCompetenceRepository;
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

            await languageCompetenceRepository.DeleteAsync(request.LanguageCompetenceId);

            return new DeleteLanguageCompetenceCommandResponse
            {
                Success = true
            };
        }
    }
}

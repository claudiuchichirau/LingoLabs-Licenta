using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.DeleteLanguageCompetence;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommandHandler: IRequestHandler<DeleteLanguageCommand, DeleteLanguageCommandResponse>
    {
        private readonly ILanguageRepository languageRepository;
        private readonly DeleteLanguageCompetenceCommandHandler deleteLanguageCompetenceCommandHandler;

        public DeleteLanguageCommandHandler(ILanguageRepository languageRepository, DeleteLanguageCompetenceCommandHandler deleteLanguageCompetenceCommandHandler)
        {
            this.languageRepository = languageRepository;
            this.deleteLanguageCompetenceCommandHandler = deleteLanguageCompetenceCommandHandler;
        }

        public async Task<DeleteLanguageCommandResponse> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteLanguageCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new DeleteLanguageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var language = await languageRepository.FindByIdAsync(request.LanguageId);

            if(!language.IsSuccess)
            {
                return new DeleteLanguageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { language.Error }
                };
            }

            var languageCompetences = language.Value.LanguageCompetences.ToList();

            foreach (LanguageCompetence languageCompetence in languageCompetences)
            {
                var deleteLanguageCompetenceCommand = new DeleteLanguageCompetenceCommand { LanguageCompetenceId = languageCompetence.LanguageCompetenceId };
                var deleteLanguageCompetenceCommandResponse = await deleteLanguageCompetenceCommandHandler.Handle(deleteLanguageCompetenceCommand, cancellationToken);

                if(!deleteLanguageCompetenceCommandResponse.Success)
                {
                    return new DeleteLanguageCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteLanguageCompetenceCommandResponse.ValidationsErrors
                    };
                }
            }

            var result = await languageRepository.DeleteAsync(request.LanguageId);

            if(!result.IsSuccess)
            {
                return new DeleteLanguageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }

            return new DeleteLanguageCommandResponse
            {
                Success = true
            };
        }
    }
}

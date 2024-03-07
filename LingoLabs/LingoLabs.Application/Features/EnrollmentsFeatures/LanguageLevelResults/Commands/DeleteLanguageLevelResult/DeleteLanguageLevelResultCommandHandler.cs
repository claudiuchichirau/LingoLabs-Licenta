using LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.DeleteChapterResult;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.DeleteLanguageLevelResult
{
    public class DeleteLanguageLevelResultCommandHandler : IRequestHandler<DeleteLanguageLevelResultCommand, DeleteLanguageLevelResultCommandResponse>
    {
        private readonly ILanguageLevelResultRepository languageLevelResultRepository;
        private readonly DeleteChapterResultCommandHandler deleteChapterResultCommandHandler;

        public DeleteLanguageLevelResultCommandHandler(ILanguageLevelResultRepository languageLevelResultRepository, DeleteChapterResultCommandHandler deleteChapterResultCommandHandler)
        {
            this.languageLevelResultRepository = languageLevelResultRepository;
            this.deleteChapterResultCommandHandler = deleteChapterResultCommandHandler;
        }
        public async Task<DeleteLanguageLevelResultCommandResponse> Handle(DeleteLanguageLevelResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteLanguageLevelResultCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new DeleteLanguageLevelResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }
            
            var languageLevelResult = await languageLevelResultRepository.FindByIdAsync(request.LanguageLevelResultId);

            if(!languageLevelResult.IsSuccess)
            {
                return new DeleteLanguageLevelResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { languageLevelResult.Error }
                };
            }

            var chapterResults = languageLevelResult.Value.ChapterResults.ToList();

            foreach (ChapterResult chapterResult in chapterResults)
            {
                var deleteChapterResultCommand = new DeleteChapterResultCommand { ChapterResultId = chapterResult.ChapterResultId };
                var deleteChapterResultCommandResponse = await deleteChapterResultCommandHandler.Handle(deleteChapterResultCommand, cancellationToken);

                if(!deleteChapterResultCommandResponse.Success)
                {
                    return new DeleteLanguageLevelResultCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteChapterResultCommandResponse.ValidationsErrors
                    };
                }   
            }

            await languageLevelResultRepository.DeleteAsync(request.LanguageLevelResultId);

            return new DeleteLanguageLevelResultCommandResponse
            {
                Success = true
            };
        }
    }
}

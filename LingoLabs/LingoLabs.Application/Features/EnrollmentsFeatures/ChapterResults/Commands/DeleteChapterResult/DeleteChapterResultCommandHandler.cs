using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.DeleteLanguageCompetenceResult;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.DeleteChapterResult
{
    public class DeleteChapterResultCommandHandler : IRequestHandler<DeleteChapterResultCommand, DeleteChapterResultCommandResponse>
    {
        private readonly IChapterResultRepository chapterResultRepository;
        private readonly DeleteLanguageCompetenceResultCommandHandler deleteLanguageCompetenceResultCommandHandler;

        public DeleteChapterResultCommandHandler(IChapterResultRepository chapterResultRepository, DeleteLanguageCompetenceResultCommandHandler deleteLanguageCompetenceResultCommandHandler)
        {
            this.chapterResultRepository = chapterResultRepository;
            this.deleteLanguageCompetenceResultCommandHandler = deleteLanguageCompetenceResultCommandHandler;
        }

        public async Task<DeleteChapterResultCommandResponse> Handle(DeleteChapterResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteChapterResultCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new DeleteChapterResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var chapterResult = await chapterResultRepository.FindByIdAsync(request.ChapterResultId);
            if(!chapterResult.IsSuccess)
            {
                return new DeleteChapterResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapterResult.Error }
                };
            }

            var languageCompetenceResults = chapterResult.Value.LanguageCompetenceResults.ToList();

            foreach (LanguageCompetenceResult languageCompetenceResult in languageCompetenceResults) 
            {
                var deleteLanguageCompetenceResultCommand = new DeleteLanguageCompetenceResultCommand { LanguageCompetenceResultId = languageCompetenceResult.LanguageCompetenceResultId };
                var deleteLanguageCompetenceResultCommandResponse = await deleteLanguageCompetenceResultCommandHandler.Handle(deleteLanguageCompetenceResultCommand, cancellationToken);

                if(!deleteLanguageCompetenceResultCommandResponse.Success)
                {
                    return new DeleteChapterResultCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteLanguageCompetenceResultCommandResponse.ValidationsErrors
                    };
                }
            }

            await chapterResultRepository.DeleteAsync(request.ChapterResultId);

            return new DeleteChapterResultCommandResponse
            {
                Success = true
            };
        }
    }
}

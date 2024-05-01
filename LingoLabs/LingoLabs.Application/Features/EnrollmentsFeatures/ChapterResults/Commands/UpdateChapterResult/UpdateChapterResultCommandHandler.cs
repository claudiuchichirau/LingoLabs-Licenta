using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.UpdateLanguageLevelResult;
using LingoLabs.Application.Persistence.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.UpdateChapterResult
{
    public class UpdateChapterResultCommandHandler : IRequestHandler<UpdateChapterResultCommand, UpdateChapterResultCommandResponse>
    {
        private readonly IChapterResultRepository chapterResultRepository;
        private readonly ILanguageLevelResultRepository languageLevelResultRepository;
        private readonly UpdateLanguageLevelResultCommandHandler updateLanguageLevelResultCommandHandler;

        public UpdateChapterResultCommandHandler(IChapterResultRepository chapterResultRepository, ILanguageLevelResultRepository languageLevelResultRepository, UpdateLanguageLevelResultCommandHandler updateLanguageLevelResultCommandHandler)
        {
            this.chapterResultRepository = chapterResultRepository;
            this.languageLevelResultRepository = languageLevelResultRepository;
            this.updateLanguageLevelResultCommandHandler = updateLanguageLevelResultCommandHandler;
        }
        public async Task<UpdateChapterResultCommandResponse> Handle(UpdateChapterResultCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateChapterResultCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new UpdateChapterResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var chapterResult = await chapterResultRepository.FindByIdAsync(request.ChapterResultId);

            if(!chapterResult.IsSuccess)
            {
                return new UpdateChapterResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapterResult.Error }
                };
            }


            chapterResult.Value.UpdateChapterResult(request.IsCompleted);

            await chapterResultRepository.UpdateAsync(chapterResult.Value);

            var languageLevelResult = await languageLevelResultRepository.FindByIdAsync(chapterResult.Value.LanguageLevelResultId);

            if (!languageLevelResult.IsSuccess)
            {
                return new UpdateChapterResultCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { languageLevelResult.Error }
                };
            }

            bool IsLanguageLevelResultCompleted = true;
            foreach(var languageChaperResult in languageLevelResult.Value.ChapterResults)
            {
                if(!languageChaperResult.IsCompleted)
                {
                    IsLanguageLevelResultCompleted = false;
                    break;
                }
            }

            if (IsLanguageLevelResultCompleted)
            {
                var updateLanguageLevelResultCommand = new UpdateLanguageLevelResultCommand
                {
                    LanguageLevelResultId = languageLevelResult.Value.LanguageLevelResultId,
                    IsCompleted = true
                };

                var updateLanguageLevelResultCommandResponse = await updateLanguageLevelResultCommandHandler.Handle(updateLanguageLevelResultCommand, cancellationToken);

                if (!updateLanguageLevelResultCommandResponse.Success)
                {
                    return new UpdateChapterResultCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = updateLanguageLevelResultCommandResponse.ValidationsErrors
                    };
                }
            }

            return new UpdateChapterResultCommandResponse
            {
                Success = true,
                UpdateChapterResult = new UpdateChapterResultDto
                {
                    IsCompleted = chapterResult.Value.IsCompleted
                }
            };
        }
    }
}

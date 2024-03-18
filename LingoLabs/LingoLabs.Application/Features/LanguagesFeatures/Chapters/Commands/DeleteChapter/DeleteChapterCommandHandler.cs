using LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.DeleteEntityTag;
using LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.DeleteLanguage;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommandHandler : IRequestHandler<DeleteChapterCommand, DeleteChapterCommandResponse>
    {
        private readonly IChapterRepository chapterRepository;
        private readonly DeleteEntityTagCommandHandler deleteEntityTagCommandHandler;

        public DeleteChapterCommandHandler(IChapterRepository chapterRepository, DeleteEntityTagCommandHandler deleteEntityTagCommandHandler)
        {
            this.chapterRepository = chapterRepository;
            this.deleteEntityTagCommandHandler = deleteEntityTagCommandHandler;
        }
        public async Task<DeleteChapterCommandResponse> Handle(DeleteChapterCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteChapterCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new DeleteChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var chapter = await chapterRepository.FindByIdAsync(request.ChapterId);
            if(!chapter.IsSuccess)
            {
                return new DeleteChapterCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { chapter.Error }
                };
            }

            var entityTags = chapter.Value.ChapterTags.ToList();

            foreach (EntityTag entityTag in entityTags)
            {
                var deleteEntityTagCommand = new DeleteEntityTagCommand { EntityTagId = entityTag.EntityTagId };
                var deleteEntityTagCommandResponse = await deleteEntityTagCommandHandler.Handle(deleteEntityTagCommand, cancellationToken);

                if (!deleteEntityTagCommandResponse.Success)
                {
                    return new DeleteChapterCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = deleteEntityTagCommandResponse.ValidationsErrors
                    };
                }
            }

            await chapterRepository.DeleteAsync(request.ChapterId);

            return new DeleteChapterCommandResponse
            {
                Success = true
            };
        }
    }
}

using LingoLabs.Domain.Entities.Enrollments;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.CreateChapterResult
{
    public class CreateChapterResultCommand: IRequest<CreateChapterResultCommandResponse>
    {
        public Guid ChapterId { get; set; }
        public List<LanguageCompetenceResult> LanguageCompetenceResults { get; set; }
    }
}

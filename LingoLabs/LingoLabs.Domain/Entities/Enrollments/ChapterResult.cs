using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Domain.Entities.Enrollments
{
    public class ChapterResult : AuditableEntity
    {
        public Guid ChapterResultId { get; private set; }
        public List<LanguageCompetenceResult> LanguageCompetenceResults { get; private set; } = new();
        public bool IsCompleted { get; private set; }
        public Guid ChapterId { get; private set; }
        public Chapter? Chapter { get; set; }
        public Guid LanguageLevelResultId { get; private set; }
        public LanguageLevelResult? LanguageLevelResult { get; set; }

        private ChapterResult(Guid chapterId, Guid languageLevelResultId)
        {
            ChapterResultId = Guid.NewGuid();
            ChapterId = chapterId;
            LanguageLevelResultId = languageLevelResultId;
            IsCompleted = false;
        }

        public static Result<ChapterResult> Create(Guid chapterId, Guid languageLevelResultId)
        {
            if(chapterId == default)
                return Result<ChapterResult>.Failure("ChapterId is required");
            if(languageLevelResultId == default)
                return Result<ChapterResult>.Failure("LanguageLevelResultId is required");
            return Result<ChapterResult>.Success(new ChapterResult(chapterId, languageLevelResultId));
        }

        public void AttachLanguageCompetenceResult(LanguageCompetenceResult languageCompetenceResult)
        {
            if(languageCompetenceResult != null)
            {
                if(LanguageCompetenceResults == null)
                    LanguageCompetenceResults = new List<LanguageCompetenceResult> { languageCompetenceResult };
                else   
                    LanguageCompetenceResults.Add(languageCompetenceResult);
            }
        }

        public void MakeCompletedChapter()
        {
            IsCompleted = true;
        }
    }
}

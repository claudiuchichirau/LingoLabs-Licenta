using LingoLabs.Domain.Entities.Enrollments;

namespace LingoLabs.Application.Persistence.Enrollments
{
    public interface ILanguageLevelResultRepository: IAsyncRepository<LanguageLevelResult>
    {
        Task<bool> CheckLanguageLevel(Guid enrollmentId, Guid languageLevelId);
    }
}

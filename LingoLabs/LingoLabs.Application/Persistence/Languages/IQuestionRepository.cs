using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Persistence.Languages
{
    public interface IQuestionRepository: IAsyncRepository<Question>
    {
    }
}

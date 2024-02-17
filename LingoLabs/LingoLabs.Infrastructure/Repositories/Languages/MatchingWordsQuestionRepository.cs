using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class MatchingWordsQuestionRepository: BaseRepository<MatchingWordsQuestion>, IMatchingWordsQuestionRepository
    {
        public MatchingWordsQuestionRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

﻿using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class QuestionRepository: BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(LingoLabsDbContext context) : base(context)
        {
        }
    }
}

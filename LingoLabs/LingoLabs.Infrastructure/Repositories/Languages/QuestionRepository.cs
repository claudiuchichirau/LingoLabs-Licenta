using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Common;
using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LingoLabs.Infrastructure.Repositories.Languages
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(LingoLabsDbContext context) : base(context)
        {
        }

        public override async Task<Result<Question>> FindByIdAsync(Guid id)
        {
            var result = await context.Questions
                .Include(l => l.QuestionChoices)
                .Include(l => l.Lesson)
                .FirstOrDefaultAsync(l => l.QuestionId == id);

            if (result == null)
            {
                return Result<Question>.Failure($"Entity with id {id} not found");
            }
            return Result<Question>.Success(result);
        }

        public async Task<Result<IReadOnlyList<Question>>> GetQuestionsByLanguageLevelIdAsync(Guid languageLevelId)
        {
            var languageLevel = await context.LanguageLevels
                .Include(l => l.LanguageChapters)
                .ThenInclude(lc => lc.languageCompetences)
                .ThenInclude(lc => lc.Lessons)
                .ThenInclude(l => l.LessonQuestions)
                .FirstOrDefaultAsync(l => l.LanguageLevelId == languageLevelId);

            if(languageLevel == null)
            {
                return Result<IReadOnlyList<Question>>.Failure($"Entity with id {languageLevelId} not found");
            }

            var questions = new List<Question>();

            foreach (var chapter in languageLevel.LanguageChapters)
            {
                foreach (var competence in chapter.languageCompetences)
                {
                    foreach (var lesson in competence.Lessons)
                    {
                        var result = await context.Questions
                            .Include(q => q.QuestionChoices)
                            .Where(q => q.LessonId == lesson.LessonId)
                            .ToListAsync();

                        questions.AddRange(result);
                    }
                }
            }

            return Result<IReadOnlyList<Question>>.Success(questions);
        }

        public async Task<Result<IReadOnlyList<Question>>> GetQuestionsByLanguageCompetenceIdAsync(Guid languageCompetenceId)
        {
            var languageCompetence = await context.LanguageCompetences
                .Include(lc => lc.Lessons)
                .ThenInclude(l => l.LessonQuestions)
                .FirstOrDefaultAsync(lc => lc.LanguageCompetenceId == languageCompetenceId);

            if (languageCompetence == null)
            {
                return Result<IReadOnlyList<Question>>.Failure($"Entity with id {languageCompetenceId} not found");
            }

            var questions = new List<Question>();

            foreach (var lesson in languageCompetence.Lessons)
            {
                var result = await context.Questions
                    .Include(q => q.QuestionChoices)
                    .Where(q => q.LessonId == lesson.LessonId)
                    .ToListAsync();

                questions.AddRange(result);
            }

            return Result<IReadOnlyList<Question>>.Success(questions);
        }

    }
}

using LingoLabs.Application.Persistence;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Infrastructure.Repositories;
using LingoLabs.Infrastructure.Repositories.Enrollments;
using LingoLabs.Infrastructure.Repositories.Languages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LingoLabs.Infrastructure.Data
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastructureToDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LingoLabsDbContext>(
                options =>
                options.UseSqlServer(configuration.GetConnectionString("LingoLabsDBConnection"),
                builder => builder.MigrationsAssembly(typeof(LingoLabsDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILanguageLevelRepository, LanguageLevelRepository>();
            services.AddScoped<ILanguageCompetenceRepository, LanguageCompetenceRepository>();
            services.AddScoped<IChapterRepository, ChapterRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IListeningLessonRepository, ListeningLessonRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IMatchingWordsQuestionRepository, MatchingWordsQuestionRepository>();
            services.AddScoped<IChoiceRepository, ChoiceRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IWordPairRepository, WordPairRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<ILanguageLevelResultRepository, LanguageLevelResultRepository>();
            services.AddScoped<IChapterResultRepository, ChapterResultRepository>();
            services.AddScoped<ILanguageCompetenceResultRepository, LanguageCompetenceResultRepository>();
            services.AddScoped<ILessonResultRepository, LessonResultRepository>();
            services.AddScoped<IQuestionResultRepository, QuestionResultRepository>();
            services.AddScoped<IReadingQuestionResultRepository, ReadingQuestionResultRepository>();
            services.AddScoped<IWritingQuestionResultRepository, WritingQuestionResultRepository>();
            services.AddScoped<IUserLanguageLevelRepository, UserLanguageLevelRepository>();
            services.AddScoped<ILearningStyleRepository, LearningStyleRepository>();


            return services;
        }
    }
}

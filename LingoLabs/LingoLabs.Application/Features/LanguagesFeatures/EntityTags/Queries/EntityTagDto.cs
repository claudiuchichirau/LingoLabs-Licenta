namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Queries
{
    public class EntityTagDto
    {
        public Guid EntityTagId { get; set; }
        public Guid? LanguageId { get; set; }
        public Guid? LanguageLevelId { get; set; }
        public Guid? ChapterId { get; set; }
        public Guid? LanguageCompetenceId { get; set; }
        public Guid? LessonId { get; set; }
        public Guid TagId { get; set; }
    }
}

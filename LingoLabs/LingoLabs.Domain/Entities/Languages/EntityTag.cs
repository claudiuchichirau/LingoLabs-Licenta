using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class EntityTag
    {
        public Guid EntityTagId { get; private set; }
        public EntityType EntityType { get; private set; }
        public Guid? LanguageId { get; private set; }
        public Language? Language { get; private set; }
        public Guid? LanguageCompetenceId { get; private set; }
        public LanguageCompetence? LanguageCompetence { get; private set; }
        public Guid? LanguageLevelId { get; private set; }
        public LanguageLevel? LanguageLevel { get; private set; }
        public Guid? ChapterId { get; private set; }
        public Chapter? Chapter { get; private set; }
        public Guid? LessonId { get; private set; }
        public Lesson? Lesson { get; private set; }
        public Guid TagId { get; private set; }
        public Tag? Tag { get; private set; }

        private EntityTag(Guid tagId)
        {
            EntityTagId = Guid.NewGuid();
            TagId = tagId;
        }

        public EntityTag EntityTagForLanguage(Guid languageId, Guid tagId, EntityType entityType)
        {
            LanguageId = languageId;
            EntityType = entityType;
            return this;
        }

        public EntityTag EntityTagForLanguageLevel(Guid languageLevelId, Guid tagId, EntityType entityType) 
        {
            LanguageLevelId = languageLevelId;
            EntityType = entityType;
            return this;
        }

        public EntityTag EntityTagForChapter(Guid chapterId, Guid tagId, EntityType entityType) 
        {
            ChapterId = chapterId;
            EntityType = entityType;
            return this;
        }

        public EntityTag EntityTagForLanguageCompetence(Guid languageCompetenceId, Guid tagId, EntityType entityType)
        {
            LanguageCompetenceId = languageCompetenceId;
            EntityType = entityType;
            return this;
        }

        public EntityTag EntityTagForLesson(Guid lessonId, Guid tagId, EntityType entityType)
        {
            LessonId = lessonId;
            EntityType = entityType;
            return this;
        }

        public static Result<EntityTag> CreateForLanguage(Guid languageId, Guid tagId, EntityType entityType)
        {
            if (languageId == Guid.Empty)
            {
                return Result<EntityTag>.Failure("Language is required");
            }

            if (tagId == Guid.Empty)
            {
                return Result<EntityTag>.Failure("Tag is required");
            }

            if (entityType != EntityType.Language)
            {
                return Result<EntityTag>.Failure("Entity type is not valid");
            }

            var entityTag = new EntityTag(tagId).EntityTagForLanguage(languageId, tagId, entityType);
            return Result<EntityTag>.Success(entityTag);
        }

        public static Result<EntityTag> CreateForLanguageLevel(Guid languageLevelId, Guid tagId, EntityType entityType)
        {
            if (languageLevelId == Guid.Empty)
            {
                return Result<EntityTag>.Failure("Language is required");
            }

            if (tagId == Guid.Empty)
            {
                return Result<EntityTag>.Failure("Tag is required");
            }

            if (entityType != EntityType.LanguageLevel)
            {
                return Result<EntityTag>.Failure("Entity type is not valid");
            }

            var entityTag = new EntityTag(tagId).EntityTagForLanguageLevel(languageLevelId, tagId, entityType);
            return Result<EntityTag>.Success(entityTag);
        }

        public static Result<EntityTag> CreateForChapter(Guid chapterId, Guid tagId, EntityType entityType)
        {
            if (chapterId == Guid.Empty)
            {
                return Result<EntityTag>.Failure("Language is required");
            }

            if (tagId == Guid.Empty)
            {
                return Result<EntityTag>.Failure("Tag is required");
            }

            if (entityType != EntityType.Chapter)
            {
                return Result<EntityTag>.Failure("Entity type is not valid");
            }

            var entityTag = new EntityTag(tagId).EntityTagForChapter(chapterId, tagId, entityType);
            return Result<EntityTag>.Success(entityTag);
        }

        public static Result<EntityTag> CreateForLanguageCompetence(Guid languageCompetenceId, Guid tagId, EntityType entityType)
        {
            if (languageCompetenceId == Guid.Empty)
            {
                return Result<EntityTag>.Failure("Language is required");
            }

            if (tagId == Guid.Empty)
            {
                return Result<EntityTag>.Failure("Tag is required");
            }

            if (entityType != EntityType.LanguageCompetence)
            {
                return Result<EntityTag>.Failure("Entity type is not valid");
            }

            var entityTag = new EntityTag(tagId).EntityTagForLanguageCompetence(languageCompetenceId, tagId, entityType);
            return Result<EntityTag>.Success(entityTag);
        }

        public static Result<EntityTag> CreateForLesson(Guid lessonId, Guid tagId, EntityType entityType)
        {
            if (lessonId == Guid.Empty)
            {
                return Result<EntityTag>.Failure("Language is required");
            }

            if (tagId == Guid.Empty)
            {
                return Result<EntityTag>.Failure("Tag is required");
            }

            if (entityType != EntityType.Lesson)
            {
                return Result<EntityTag>.Failure("Entity type is not valid");
            }

            var entityTag = new EntityTag(tagId).EntityTagForLesson(lessonId, tagId, entityType);
            return Result<EntityTag>.Success(entityTag);
        }
    }
}

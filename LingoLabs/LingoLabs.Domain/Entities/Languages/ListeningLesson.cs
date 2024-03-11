using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class ListeningLesson : Lesson
    {
        public string TextScript { get; private set; }
        public List<string> Accents { get; private set; }

        private ListeningLesson(string lessonTitle, LanguageCompetenceType lessonType, Guid languageCompetenceId, string textScript, List<string> accents) : base(lessonTitle, lessonType, languageCompetenceId)
        {
            LessonId = Guid.NewGuid();
            LessonTitle = lessonTitle;
            LessonType = lessonType;
            LanguageCompetenceId = languageCompetenceId;
            TextScript = textScript;
            Accents = accents;
        }

        public static Result<ListeningLesson> Create(string lessonTitle, LanguageCompetenceType lessonType, Guid languageCompetenceId, string textScript, List<string> accents)
        {
            if(string.IsNullOrWhiteSpace(lessonTitle))
                return Result<ListeningLesson>.Failure("LessonTitle is required");

            if (!IsValidLessonType(lessonType))
                return Result<ListeningLesson>.Failure("Invalid LessonType");

            if (languageCompetenceId == default)
                return Result<ListeningLesson>.Failure("Invalid LanguageCompetenceId");
            
            if (string.IsNullOrWhiteSpace(textScript))
                return Result<ListeningLesson>.Failure("TextScript is required");

            if (accents == null || accents.Count == 0)
                return Result<ListeningLesson>.Failure("Accents is required");

            return Result<ListeningLesson>.Success(new ListeningLesson(lessonTitle, lessonType, languageCompetenceId, textScript, accents));
        }

        public void AttachAccent(string accent)
        {
            if (!string.IsNullOrWhiteSpace(accent))
            {
                if (Accents == null)
                    Accents = new List<string> { accent };
                else
                    Accents.Add(accent);
            }
        }

        public void UpdateListeningLanguage(string lessonTitle, string lessonDescription, string lessonRequirement, string lessonContent, byte[] imageData, string videoLink, string textScript, List<string> accents)
        {
            LessonTitle = lessonTitle;
            LessonDescription = lessonDescription;
            LessonRequirement = lessonRequirement;
            LessonContent = lessonContent;
            LessonImageData = imageData;
            LessonVideoLink = videoLink;
            TextScript = textScript;
            if (accents != null && accents.Count > 0)
                Accents = accents;
        }

        private static bool IsValidLessonType(LanguageCompetenceType languageCompetenceType)
        {
            return languageCompetenceType == LanguageCompetenceType.Grammar ||
                   languageCompetenceType == LanguageCompetenceType.Listening ||
                   languageCompetenceType == LanguageCompetenceType.Reading ||
                   languageCompetenceType == LanguageCompetenceType.Writing;
        }
    }
}

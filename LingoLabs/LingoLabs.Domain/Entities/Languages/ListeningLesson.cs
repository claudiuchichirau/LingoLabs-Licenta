using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class ListeningLesson : Lesson
    {
        public List<byte[]> AudioContents { get; private set; }
        public List<string> Accents { get; private set; }

        private ListeningLesson(string lessonTitle, LanguageCompetenceType lessonType, List<byte[]> audioContents, List<string> accents) : base(lessonTitle, lessonType)
        {
            AudioContents = audioContents;
            Accents = accents;
        }

        public static Result<ListeningLesson> Create(string lessonTitle, LanguageCompetenceType lessonType, List<byte[]> audioContents, List<string> accents)
        {
            if(string.IsNullOrWhiteSpace(lessonTitle))
                return Result<ListeningLesson>.Failure("LessonTitle is required");

            if (!IsValidLessonType(lessonType))
                return Result<ListeningLesson>.Failure("Invalid LessonType");
            
            if (audioContents == null || audioContents.Count == 0)
                return Result<ListeningLesson>.Failure("AudioContents is required");

            if (accents == null || accents.Count == 0)
                return Result<ListeningLesson>.Failure("Accents is required");

            return Result<ListeningLesson>.Success(new ListeningLesson(lessonTitle, lessonType, audioContents, accents));
        }

        public void AttachAudioContent(byte[] audioContent)
        {
            if (audioContent != null)
            {
                if (AudioContents == null)
                    AudioContents = new List<byte[]> { audioContent };
                else
                    AudioContents.Add(audioContent);
            }
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

        public void Update(string lessonTitle, LanguageCompetenceType lessonType, List<byte[]> audioContents, List<string> accents)
        {
            if (!string.IsNullOrWhiteSpace(lessonTitle))
                base.LessonTitle = lessonTitle;
            if (IsValidLessonType(lessonType))
                base.LessonType = lessonType;
            if (audioContents != null && audioContents.Count > 0)
                AudioContents = audioContents;
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

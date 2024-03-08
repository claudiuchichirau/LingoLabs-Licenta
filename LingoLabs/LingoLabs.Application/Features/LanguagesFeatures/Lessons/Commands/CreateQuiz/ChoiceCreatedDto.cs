

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz
{
    public class ChoiceCreatedDto
    {
        public Guid ChoiceId { get; set; }
        public ChoiceDtoForQuiz? Choice { get; set; }
    }
}
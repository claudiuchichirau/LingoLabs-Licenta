using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateQuiz
{
    public class ChoiceUpdatedDto
    {
        public Guid ChoiceId { get; set; }
        public ChoiceDtoForQuiz? Choice { get; set; }
    }
}
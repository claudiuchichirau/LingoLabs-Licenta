namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class QuestionElement
    {
        public Guid QuestionId { get; set; }
        public string QuestionRequirement { get; set; } = string.Empty;
        public List<ChoiceElement>? QuestionChoices { get; set; }
        public string? QuestionImageData { get; set; }
        public string? QuestionVideoLink { get; set; } 
    }
}

namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class ChoiceElement
    {
        public Guid ChoiceId { get; set; }
        public string ChoiceContent { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class AccentViewModel
    {
        public Guid AccentId { get; set; }
        public string AccentName { get; set; } = string.Empty;
        public bool IsChecked { get; set; } = false;
    }
}

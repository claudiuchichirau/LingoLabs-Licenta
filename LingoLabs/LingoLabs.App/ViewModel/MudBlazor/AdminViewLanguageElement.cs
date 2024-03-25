using System.Text.Json.Serialization;

namespace LingoLabs.App.ViewModel.MudBlazor
{
    public class AdminViewLanguageElement
    {
        public string LanguageName { get; set; } = string.Empty;
        public string? LanguageDescription { get; set; } 
        public string? LanguageVideoLink { get; set; }
        public string? LanguageFlag { get; set; }
    }
}

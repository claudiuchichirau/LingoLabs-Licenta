using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels.LanguageViewModels
{
    public class LanguageViewModel
    {
        public Guid LanguageId { get; set; }
        [Required(ErrorMessage = "Language name is required")]
        public string LanguageName { get; set; } = string.Empty;
        public string LanguageDescription { get; set; } = string.Empty;
        public string LanguageVideoLink { get; set; } = string.Empty;
        public byte[] LanguageFlag { get; set; } = [];
    }
}

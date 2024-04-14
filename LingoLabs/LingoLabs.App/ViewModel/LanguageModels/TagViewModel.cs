using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class TagViewModel
    {
        public Guid TagId { get; set; }
        [Required(ErrorMessage = "Lesson Title is required")]
        public string TagContent { get; set; } = string.Empty;
    }
}
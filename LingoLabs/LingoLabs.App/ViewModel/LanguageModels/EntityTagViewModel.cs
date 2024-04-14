using System.ComponentModel.DataAnnotations;

namespace LingoLabs.App.ViewModel.LanguageModels
{
    public class EntityTagViewModel
    {
        public Guid EntityTagId { get; set; }
        [Required(ErrorMessage = "EntityId is required")]
        public Guid EntityId { get; set; }
        [Required(ErrorMessage = "TagId is required")]
        public Guid TagId { get; set; }
        [Required(ErrorMessage = "EntityType is required")]
        public EntityTypeViewModel EntityType { get; set; }
    }
}
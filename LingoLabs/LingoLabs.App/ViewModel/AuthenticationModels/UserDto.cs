namespace LingoLabs.App.ViewModel.AuthenticationModels
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public string DefaultLanguage { get; set; } = string.Empty;
    }
}

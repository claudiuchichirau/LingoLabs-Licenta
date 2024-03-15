namespace LingoLabs.Application.Models.Identity
{
    public class UserInfoModel
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string DefaultLanguage { get; private set; }

        public UserInfoModel(string username, string email, string phoneNumber, string firstName, string lastName)
        {
            UserName = username;
            Email = email;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
        }

        public void UpdateUserInfo(string username, string email, string phoneNumber, string firstName, string lastName, string defaultLanguage)
        {
            UserName = username;
            Email = email;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            DefaultLanguage = defaultLanguage;
        }
    }
}

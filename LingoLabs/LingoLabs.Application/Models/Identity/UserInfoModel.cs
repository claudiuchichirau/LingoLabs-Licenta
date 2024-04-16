namespace LingoLabs.Application.Models.Identity
{
    public class UserInfoModel
    {
        public Guid UserId { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public UserInfoModel(Guid userId, string userName, string email, string phoneNumber, string firstName, string lastName)
        { 
            UserId = userId;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}

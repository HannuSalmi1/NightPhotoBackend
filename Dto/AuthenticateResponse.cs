using NightPhotoBackend.Models;

namespace NightPhotoBackend.Dto
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        




        public AuthenticateResponse(UserModel user)

        {
            Id = user.Id;
            FirstName = user.Firstname;
            LastName = user.Lastname;
            Username = user.Username;
            
        }
    }
}

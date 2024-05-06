using NightPhotoBackend.Dto;

using NightPhotoBackend.Models;

namespace NightPhotoBackend.Services
{
    public interface IUserservice
    {

        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<UserModel> GetAll();
        UserModel GetById(int id);

    }
}

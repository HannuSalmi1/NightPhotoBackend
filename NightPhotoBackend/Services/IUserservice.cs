using NightPhotoBackend.Entities;
using NightPhotoBackend.Models;

namespace NightPhotoBackend.Services
{
    public interface IUserservice
    {

        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<UserEntity> GetAll();
        UserEntity GetById(int id);

    }
}

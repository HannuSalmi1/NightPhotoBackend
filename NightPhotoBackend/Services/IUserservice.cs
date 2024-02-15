using NightPhotoBackend.Dto;
using NightPhotoBackend.Entities;
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

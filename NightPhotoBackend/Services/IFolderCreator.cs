using NightPhotoBackend.Entities;

namespace NightPhotoBackend.Services
{
    public interface IFolderCreator
    {
        void CreateFolder(UserModel model);

        public List<string> RetrieveImagesPaths();
    }
    
}

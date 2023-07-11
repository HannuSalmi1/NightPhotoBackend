using NightPhotoBackend.Entities;

namespace NightPhotoBackend.Services
{
    public interface IFolderCreator
    {
        void CreateFolder(UserEntity model);

        public List<string> RetrieveImagesPaths();
    }
    
}

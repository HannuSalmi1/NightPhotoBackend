using NightPhotoBackend.Entities;

namespace NightPhotoBackend.Services
{
    public class FolderCreator : IFolderCreator
    {
        public void CreateFolder(UserEntity user)
        {
            if (user.Username == null)
            {
                Console.WriteLine("Username is null");
            }
            else
            {
                String folderName = user.Username;
                String folderPath = Path.Combine("wwwroot\\Images\\", folderName);
                Console.WriteLine(folderPath);
                Directory.CreateDirectory(folderPath);
            }

        }
    }
}

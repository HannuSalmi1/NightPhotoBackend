using NightPhotoBackend.Models;
using System.Collections.Generic;
using System.Drawing;

namespace NightPhotoBackend.Services
{
    public class FolderCreator : IFolderCreator
    {
        public void CreateFolder(UserModel user)
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
        public List <string> RetrieveImagesPaths()
        {
            string path = "wwwroot\\Images\\";
            List<string> imagePaths = new List<string>();
            var paths = Directory.GetDirectories(path);
            
            foreach(string folder in paths)
            {
               
                var files = Directory.GetFiles(folder);
                foreach (string file in files)
                { imagePaths.Add(file); }
               
            }

            
            return imagePaths;

          
        }
    }
}

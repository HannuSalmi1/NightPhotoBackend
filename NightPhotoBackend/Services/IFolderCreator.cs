﻿using NightPhotoBackend.Models;

namespace NightPhotoBackend.Services
{
    public interface IFolderCreator
    {
        void CreateFolder(UserModel model);
    }
}
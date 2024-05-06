﻿using NightPhotoBackend.Entities;

namespace NightPhotoBackend.Dto
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UserEntity user)
        {
            Id = user.Id;
            FirstName = user.Firstname;
            LastName = user.Lastname;
            Username = user.Username;
            
        }
    }
}

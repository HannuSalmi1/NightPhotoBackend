﻿using System;
using System.Collections.Generic;

namespace NightPhotoBackend.Models;

public partial class UsersTable
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}

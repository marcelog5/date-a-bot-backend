﻿namespace Domain.Users
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}

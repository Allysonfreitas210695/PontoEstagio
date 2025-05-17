using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace PontoEstagio.Domain.Helpers
{
    public class PasswordHelper
    {
        private static readonly string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";

        public static string GenerateRandomPassword(int length = 8)
        {
            string password;
            do
            {
                password = GenerateRandomPasswordInternal(length);
            } while (!IsValidPassword(password));

            return password;
        }

        private static string GenerateRandomPasswordInternal(int length)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var password = new char[length];
                var randomBytes = new byte[length];
                rng.GetBytes(randomBytes);  

                for (int i = 0; i < length; i++)
                {
                    password[i] = Characters[randomBytes[i] % Characters.Length];
                }

                return new string(password);
            }
        }

        private static bool IsValidPassword(string password)
        {
             return !string.IsNullOrWhiteSpace(password) &&
                   password.Length >= 8 &&
                   Regex.IsMatch(password, "[A-Z]") &&   
                   Regex.IsMatch(password, "[a-z]") &&   
                   Regex.IsMatch(password, "[0-9]") &&   
                   Regex.IsMatch(password, @"[\W_]");    
        }
    }
}

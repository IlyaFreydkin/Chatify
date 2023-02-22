using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ChatifyProject.Application.Model
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {

#pragma warning disable CS8618
        protected User() { }
#pragma warning restore CS8618

        public User(string name, string email, Userrole role)
        {
            Name = name;
            Email = email;
            Role = role;
        }
        public int Id { get; private set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Userrole Role { get; set; }
        public DateTime Created { get; set; }
        public Userprofile? Profile {get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }

        [MemberNotNull(nameof(Salt), nameof(PasswordHash))]
        public void SetPassword(string password)
        {
            Salt = GenerateRandomSalt();
            PasswordHash = CalculateHash(password, Salt);
        }

        public bool CheckPassword(string password) => PasswordHash == CalculateHash(password, Salt);

        private string GenerateRandomSalt(int length = 128)
        {
            byte[] salt = new byte[length / 8];
            using (System.Security.Cryptography.RandomNumberGenerator rnd =
                System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        private string CalculateHash(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            System.Security.Cryptography.HMACSHA256 myHash =
                new System.Security.Cryptography.HMACSHA256(saltBytes);

            byte[] hashedData = myHash.ComputeHash(passwordBytes);

            // Das Bytearray wird als Hexstring zurückgegeben.
            return Convert.ToBase64String(hashedData);
        }
    }
}

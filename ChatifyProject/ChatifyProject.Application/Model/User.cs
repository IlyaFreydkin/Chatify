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
    }
}

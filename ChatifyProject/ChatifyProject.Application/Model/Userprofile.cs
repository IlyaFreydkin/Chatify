using System;
using System.ComponentModel.DataAnnotations;

namespace ChatifyProject.Application.Model
{
    public class Userprofile
    {

#pragma warning disable CS8618
        protected Userprofile() { }
#pragma warning restore CS8618

        public Userprofile(string firstname, string lastname, User user, string? description = null)
        {
            Firstname = firstname;
            Lastname = lastname;
            Description = description;
            UserId = user.Id;
            User = user;
        }

        [Key]
        public int UserId { get; private set; }
        public User User { get; set; }
        public Guid Guid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Description { get; set; }
    }
}

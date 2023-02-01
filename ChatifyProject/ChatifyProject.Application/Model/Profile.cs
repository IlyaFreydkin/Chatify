using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TripleAProject.Application.Model
{
    public class Profile
    {

#pragma warning disable CS8618
        protected Profile() { }
#pragma warning restore CS8618

        public Profile(string firstname, string lastname, string description)
        {
            Firstname = firstname;
            Lastname = lastname;
            Description = description;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        [MaxLength(20)]    
        public string Firstname { get; set; }
        [MaxLength(20)]
        public string Lastname { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [Range(0,1)]
        public Guid Guid { get; set; }
        public User User {get; set; }
}

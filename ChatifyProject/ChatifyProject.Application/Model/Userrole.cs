using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatifyProject.Application.Model
{
    public enum Userrole
    {
        Admin, 
        User
    }
}

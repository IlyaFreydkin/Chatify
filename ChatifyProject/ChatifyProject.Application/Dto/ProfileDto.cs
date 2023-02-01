using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TripleAProject.Webapi.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace TripleAProject.Application.Dto
{
    public record ProfileDto(
    Guid Guid,

    [StringLength(20, MinimumLength = 1, ErrorMessage = "Die Länge der Vorname ist ungültig.")]
    string Firstname,
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Die Länge der Nachname ist ungültig.")]
    string Lastname,
    [StringLength(255, MinimumLength = 0, ErrorMessage = "Die Länge der Beschreibung ist ungültig.")]
    string Description,

    Guid UserGuid) : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = validationContext.GetRequiredService<ChatifyContext>();
            if (!db.Users.Any(a => a.Guid == UserGuid))
            {
                yield return new ValidationResult("User does not exist", new[] { nameof(UserGuid) });
            }
        }
    }
}


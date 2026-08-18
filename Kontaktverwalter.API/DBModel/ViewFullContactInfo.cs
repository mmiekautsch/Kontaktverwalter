using System;
using System.Collections.Generic;

namespace Kontaktverwalter.API.DBModel;

public partial class ViewFullContactInfo
{
    public long IdPerson { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? PostalCode { get; set; }

    public string? City { get; set; }

    public string? StreetName { get; set; }

    public string? StreetNumber { get; set; }

    public string? Country { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Type { get; set; }
}

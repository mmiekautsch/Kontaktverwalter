using System;
using System.Collections.Generic;

namespace Kontaktverwalter.DBModel;

public partial class Person
{
    public long IdPerson { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? LastNameUpperCase { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<PhoneContact> PhoneContacts { get; set; } = new List<PhoneContact>();
}

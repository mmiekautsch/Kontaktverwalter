using System;
using System.Collections.Generic;

namespace Kontaktverwalter.API.DBModel;

public partial class Address
{
    public long IdAddress { get; set; }

    public string PostalCode { get; set; } = null!;

    public string City { get; set; } = null!;

    public string StreetName { get; set; } = null!;

    public string StreetNumber { get; set; } = null!;

    public string Country { get; set; } = null!;

    public long FkPerson { get; set; }

    public virtual Person FkPersonNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Kontaktverwalter.API.DBModel;

public partial class PhoneContact
{
    public long IdPhoneContact { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Type { get; set; } = null!;

    public long FkPerson { get; set; }

    public virtual Person FkPersonNavigation { get; set; } = null!;
}

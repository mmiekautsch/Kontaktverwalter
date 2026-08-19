namespace Kontaktverwalter.Shared.DTO
{
    public class ContactDetailsDto
    {
        public long IdPerson { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public List<AddressDto> Addresses { get; set; } = [];
        public List<PhoneContactDto> PhoneContacts { get; set; } = [];
    }
}
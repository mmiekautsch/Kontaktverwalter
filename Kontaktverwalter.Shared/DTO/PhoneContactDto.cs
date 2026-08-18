namespace Kontaktverwalter.Shared.DTO
{
    public class PhoneContactDto
    {
        public long IdPhoneContact { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Type { get; set; } = null!;
    }
}
namespace Kontaktverwalter.Shared.DTO
{
    public class AddressDto
    {
        public long IdAddress { get; set; }
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string StreetNumber { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
using Labb2_Shared.Models;

namespace Labb2_Shared.Dtos
{
    public class CustomerUpdateDto
    {
        public int CustomerId { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Email { get; set; }

        public long? PhoneNo { get; set; }

        public int? AdressId { get; set; }

        public virtual Adress? Adress { get; set; }
    }
}

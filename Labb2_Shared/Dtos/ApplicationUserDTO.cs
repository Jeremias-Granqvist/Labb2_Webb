using Labb2_Shared.Models;

namespace Labb2_Shared.Dtos
{
    public class ApplicationUserDTO
    {
        public int UserId { get; set; } // Map this to ApplicationUser.UserId
        public string? Firstname { get; set; } // Map this to ApplicationUser.Name (split if necessary)
        public string? Lastname { get; set; } // Optional, if needed to split Name
        public string? Email { get; set; } // Map this to ApplicationUser.Email
        public string? PhoneNo { get; set; } // Map this to ApplicationUser.PhoneNumber
        public int? AdressId { get; set; } // Map this to ApplicationUser.AddressId
        public virtual Adress? Adress { get; set; } // Adress mapping (from ApplicationUser.Adress)
        public virtual ICollection<OrderDto> Orders { get; set; } = new List<OrderDto>(); // Orders mapping (from ApplicationUser.Orders)
    }
}

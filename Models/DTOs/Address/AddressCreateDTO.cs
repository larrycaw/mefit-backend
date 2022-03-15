using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.DTOs.Address
{
    public class AddressCreateDTO
    {
        [MaxLength(50)]
        public string AddressLine1 { get; set; }
        [MaxLength(50)]
        public string AddressLine2 { get; set; }
        [MaxLength(50)]
        public string AddressLine3 { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
    }
}

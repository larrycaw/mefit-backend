using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.Domain
{
    public class Address
    {
        public int Id { get; set; }

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
        public string Contry { get; set; }
    }
}

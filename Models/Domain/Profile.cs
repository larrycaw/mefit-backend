using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.Domain
{
    public class Profile
    {
        [Required]
        public string Id { get; set; }
        public int? Weight { get; set; }
        public int? Height { get; set; }
        [MaxLength(200)]
        public string MedicalConditions { get; set; }
        [MaxLength(100)]
        public string Disabilities { get; set; }
    }
}

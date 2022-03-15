using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.DTOs.Profile
{
    public class ProfileCreateDTO
    {
        public string Id { get; set; }
        public int? Weight { get; set; }
        public int? Height { get; set; }
        [MaxLength(200)]
        public string MedicalConditions { get; set; }
        [MaxLength(100)]
        public string Disabilities { get; set; }
    }
}
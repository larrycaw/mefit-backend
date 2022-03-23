using System.ComponentModel.DataAnnotations;
using MeFit.Models.Domain;

namespace MeFit.Models.DTOs.Profile
{
    public class ProfileUpdateDTO
    {
        [MaxLength(36)]
        public string Id { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        [MaxLength(200)]
        public string MedicalConditions { get; set; }
        [MaxLength(100)]
        public string Disabilities { get; set; }
    }
}
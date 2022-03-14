using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.Domain
{
    public class Profile
    {
        [Required]
        public string Id { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public int? ProgramId { get; set; }
        public MFProgram Program { get; set; }
        public int? WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public int? SetId { get; set; }
        public Set Set { get; set; }
        public int? Weight { get; set; }
        public int? Height { get; set; }
        [MaxLength(200)]
        public string MedicalConditions { get; set; }
        [MaxLength(100)]
        public string Disabilities { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.DTOs.Exercise
{
    public class ExerciseCreateDTO
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string TargetMuscleGroup { get; set; }
        [MaxLength(50)]
        public string ImageURL { get; set; }
        [MaxLength(50)]
        public string VideoURL { get; set; }
    }
}

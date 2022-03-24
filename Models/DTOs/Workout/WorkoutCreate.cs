using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.DTOs.Workout
{
    public class WorkoutCreateDTO
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
    }
}


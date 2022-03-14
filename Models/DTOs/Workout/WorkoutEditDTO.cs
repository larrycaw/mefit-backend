using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.DTOs.Workout
{
    public class WorkoutEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
        public bool Complete { get; set; }
    }
}

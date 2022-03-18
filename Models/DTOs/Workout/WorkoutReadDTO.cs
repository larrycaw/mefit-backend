using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.DTOs.Workout
{
    public class WorkoutReadDTO
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
        public bool Complete { get; set; }
        public List<int> Sets { get; set; }
        public List<int> Goals { get; set; }
        public List<int> Programs { get; set; }

    }
}

using System.Collections.Generic;

namespace MeFit.Models.DTOs.Set
{
    public class SetReadDTO
    {
        public int Id { get; set; }
        public int ExerciseRepetitions { get; set; }
        public int ExerciseId { get; set; }
        public List<int> Workouts { get; set; }
    }
}

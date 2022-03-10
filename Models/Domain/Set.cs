using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.Domain
{
    public class Set
    {
        public int Id { get; set; }
        public int ExerciseRepetitions { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.Domain
{
    public class Workout
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
        public ICollection<Set> Sets { get; set; }
        //public ICollection<Goal> Goals { get; set; }
        public ICollection<MFProgram> Programs { get; set; }

        public virtual ICollection<GoalWorkouts> WorkoutGoals{ get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFit.Models.Domain
{
    [Keyless]
    public class GoalWorkouts
    {
        [ForeignKey("WorkoutId")]
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }

        [ForeignKey("GoalId")]
        public int GoalId { get; set; }
        public Goal Goal { get; set; }
        public bool Completed { get; set; }
    }
}

namespace MeFit.Models.DTOs.WorkoutGoals
{
    public class WorkoutGoalsEditDTO
    {
        public int WorkoutId { get; set; }
        public int GoalId { get; set; }
        public bool Completed { get; set; }
    }
}

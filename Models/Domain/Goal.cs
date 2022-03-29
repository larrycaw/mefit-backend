using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.Domain
{
    public class Goal
    {
        public int Id { get; set; }
        public DateTime ProgramEndDate { get; set; }
        public bool Achieved { get; set; }
        public int? ProgramId { get; set; }
        public MFProgram Program { get; set; }
        public string ProfileId { get; set; }
        public Profile Profile { get; set; }
        public virtual ICollection<GoalWorkouts> WorkoutGoals { get; set; }

    }
}

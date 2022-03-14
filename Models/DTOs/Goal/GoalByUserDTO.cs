using System;
using System.Collections.Generic;

namespace MeFit.Models.DTOs.Goal
{
    public class GoalByUserDTO
    {
        public DateTime ProgramEndDate { get; set; }
        public bool Achieved { get; set; }
        public int? ProgramId { get; set; }
        public ICollection<int> Workouts { get; set; }
    }
}

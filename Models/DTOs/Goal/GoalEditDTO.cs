using System;

namespace MeFit.Models.DTOs.Goal
{
    public class GoalEditDTO
    {
        public int Id { get; set; }
        public DateTime ProgramEndDate { get; set; }
        public bool Achieved { get; set; }
        public string ProfileId { get; set; }
    }
}

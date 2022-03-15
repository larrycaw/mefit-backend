using System;
using System.Collections.Generic;

namespace MeFit.Models.DTOs.Goal
{
    public class GoalCreateDTO
    {
        public DateTime ProgramEndDate { get; set; }
        public int? ProgramId { get; set; }
        public string ProfileId { get; set; }
    }
}

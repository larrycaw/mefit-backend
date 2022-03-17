using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MeFit.Models.Domain;

namespace MeFit.Models.DTOs
{
    public class ProgramReadDTO
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Category { get; set; }
        public List<int> Workouts { get; set; }
        public List<string> WorkoutNames { get; set; }
    }
}

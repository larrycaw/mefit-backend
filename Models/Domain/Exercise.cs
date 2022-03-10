using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.Domain
{
    public class Exercise
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string TargetMuscleGroup { get; set; }
        [MaxLength(50)]
        public string ImageURL { get; set; }
        [MaxLength(50)]
        public string VideoURL { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.DTOs.Program
{
    public class ProgramEditDTO
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Category { get; set; }
    }
}

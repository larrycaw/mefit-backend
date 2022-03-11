using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.DTOs.Program
{
    public class ProgramCreateDTO
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Category { get; set; }
    }
}

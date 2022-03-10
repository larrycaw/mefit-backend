using System.ComponentModel.DataAnnotations;

namespace MeFit.Models.Domain
{
    public class MFProgram
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Category { get; set; }
    }
}

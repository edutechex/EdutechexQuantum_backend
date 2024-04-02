using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EdutechexQuantum.Model
{
    public class Partner
    {
        [Key]
        public int Id { get; set; }
        public string? type { get; set; }
        [NotMapped]
        public IFormFile imageFile { get; set; }
        public string? image { get; set; }
        public string? name { get; set; }
    }
}

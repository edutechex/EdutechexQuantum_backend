using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EdutechexQuantum.Model
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string? content { get; set; }
        [NotMapped]
        public IFormFile imageFile { get; set; }
        public string? image { get; set; }
        public string? title { get; set; }
    }
}
                                 
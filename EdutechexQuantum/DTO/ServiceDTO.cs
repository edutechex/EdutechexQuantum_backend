using System.ComponentModel.DataAnnotations.Schema;

namespace EdutechexQuantum.DTO
{
   
    public class AddService
    {
        public string? title { get; set; }
        public string? content { get; set; }
        [NotMapped]
        public IFormFile? imageFile { get; set; }
    }
    public class EditService
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? imageFile { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
    }
    public class deleteService
    {
        public int Id { get; set; }
    }
}

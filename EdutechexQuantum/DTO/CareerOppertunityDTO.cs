using System.ComponentModel.DataAnnotations.Schema;

namespace EdutechexQuantum.DTO
{
 
    public class AddCareerOppertunity
    {
        public string? title { get; set; }
        public string? about { get; set; }
        [NotMapped]
        public IFormFile? imageFile { get; set; }
    }
    public class EditCareerOppertunity
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? imageFile { get; set; }
        public string? title { get; set; }
        public string? about { get; set; }
    }
    public class deleteCareerOppertunity
    {
        public int Id { get; set; }
    }
}

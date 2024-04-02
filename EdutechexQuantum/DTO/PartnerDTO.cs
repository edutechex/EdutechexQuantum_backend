using System.ComponentModel.DataAnnotations.Schema;

namespace EdutechexQuantum.DTO
{
    
    public class AddPartner
    {
        public string? type { get; set; }
        public string? name { get; set; }
        [NotMapped]
        public IFormFile? imageFile { get; set; }
    }
    public class EditPartner
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? imageFile { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
    }
    public class deletePartner
    {
        public int Id { get; set; }
    }
}

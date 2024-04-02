using System.ComponentModel.DataAnnotations.Schema;

namespace EdutechexQuantum.DTO
{
    public class AddTeam
    {
        public string? name { get; set; }
        public string? about { get; set; }
        [NotMapped]
        public IFormFile? imageFile { get; set; }
    }
    public class EditTeam
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? imageFile { get; set; }
        public string? name { get; set; }
        public string? about { get; set; }
    }
    public class deleteTeam
    {
        public int Id { get; set; }
    }
}

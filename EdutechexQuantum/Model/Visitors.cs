using System.ComponentModel.DataAnnotations;

namespace EdutechexQuantum.Model
{
    public class Visitors
    {
        [Key]
        public int Id { get; set; }
        public string? IPaddress { get; set; }
        public string? city { get; set; }
        public string? region { get; set; }
        public string? post_code { get; set; }
        public string? country { get; set; }
        public DateTime date { get; set; }
    }
}

namespace EdutechexQuantum.DTO
{
    public class addAdmin
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Admintype { get; set; }
    }
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class deleteAdmin
    {
        public int Id { get; set; }
    }
}

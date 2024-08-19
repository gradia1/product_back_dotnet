namespace MyApiProject.Models
{
    public class Login
    {
        public int Id { get; set; }
        public required string User { get; set; }
        public required string Password { get; set; }
    }
}
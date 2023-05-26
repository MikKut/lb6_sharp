namespace lb6_server.Models.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public decimal Account { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
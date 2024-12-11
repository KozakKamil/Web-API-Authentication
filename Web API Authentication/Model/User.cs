namespace Web_API_Authentication.Model;

    public class User
    {
        public required string UserName { get; set; }
        public required string EmailAddress { get; set; }
        public required string Password { get; set; }
        public string? GivenName { get; set; }
        public string? SurName { get; set; }
        public required string Role { get; set; }
    }


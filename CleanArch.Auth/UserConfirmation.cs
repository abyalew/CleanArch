namespace CleanArch.Auth
{
    public class UserConfirmation
    {
        public UserConfirmation(string id, string confimarionToken)
        {
            Id = id;
            ConfirmationToken = confimarionToken;
        }
        public string Id { get; set; }
        public string ConfirmationToken { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}

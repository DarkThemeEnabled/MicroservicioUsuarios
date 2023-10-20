namespace Domain.DTO
{
    public class UserLoggedOutEvent
    {
        public string Username { get; set; }

        public UserLoggedOutEvent(string username)
        {
            Username = username;
        }
    }
}

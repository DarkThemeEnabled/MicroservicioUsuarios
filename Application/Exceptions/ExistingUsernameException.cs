namespace Application.Exceptions
{
    public class ExistingUsernameException : Exception
    {
        public ExistingUsernameException(string message) : base(message)
        {
        }
    }
}

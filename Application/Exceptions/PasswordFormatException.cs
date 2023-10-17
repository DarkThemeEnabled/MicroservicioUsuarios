namespace Application.Exceptions
{
    public class PasswordFormatException : Exception
    {
        public PasswordFormatException(string message) : base(message) { }
    }
}

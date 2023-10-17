namespace Application.Exceptions
{
    public class ExistingMailException : Exception
    {
        public ExistingMailException(string message) : base(message)
        {
        }
    }
}

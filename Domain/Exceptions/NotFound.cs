namespace Domain.Exceptions
{
    public class NotFound : Exception
    {
        public NotFound() : base()
        {
        }

        public NotFound(string message) : base(message)
        {
        }

        public NotFound(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

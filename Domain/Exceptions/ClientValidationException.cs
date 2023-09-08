namespace Domain.Exceptions
{
    public class ClientValidationException : DomainException
    {
        public ClientValidationException(string message) : base(message)
        {
        }
    }
}

namespace TND.Domain.Exceptions
{
    public class NotFoundException(string message) : CustomException(message)
    {
        public override string Title => "Resource not found";
    }
}

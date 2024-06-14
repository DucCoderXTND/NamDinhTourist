namespace TND.Domain.Exceptions
{
    public class CannotCancelBookingException(string message) : ConflictException(message)
    {
        public override string Title => "Cannot delete booking";
    }
}

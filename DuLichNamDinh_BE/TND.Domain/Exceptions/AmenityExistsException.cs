namespace TND.Domain.Exceptions
{
    public class AmenityExistsException(string message) : ConflictException(message)
    {
        public override string Title => "Tiện ích đã tồn tại";
    }
}

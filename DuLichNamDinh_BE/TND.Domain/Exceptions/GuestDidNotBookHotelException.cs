namespace TND.Domain.Exceptions
{
    public class GuestDidNotBookHotelException(string message) : ConflictException(message)
    {
        public override string Title => "Khách chưa đặt phòng nào trong khách sạn";
    }
}

namespace TND.Domain.Exceptions
{
    public class HotelLocationReplicationException(string message) : ConflictException(message)
    {
        public override string Title => "Có một khách sạn khác ở cùng vị trí";
    }
}

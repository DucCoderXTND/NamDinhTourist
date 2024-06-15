namespace TND.Domain.Exceptions
{
    public class RoomNotAvailableException(string message) : BadRequestException(message)
    {
        public override string Title => "Phòng không có sẵn";
    }
}

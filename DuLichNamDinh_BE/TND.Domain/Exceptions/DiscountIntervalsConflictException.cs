namespace TND.Domain.Exceptions
{
    public class DiscountIntervalsConflictException(string message) : ConflictException(message)
    {
        public override string Title => "Xung đột khoảng thời gian kích hoạt giảm giá";
    }
}

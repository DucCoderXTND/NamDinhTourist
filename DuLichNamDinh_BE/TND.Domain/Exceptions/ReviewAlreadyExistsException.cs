namespace TND.Domain.Exceptions
{
    public class ReviewAlreadyExistsException(string message) : ConflictException(message)
    {
        public override string Title => "Khách sạn đã được đánh giá rồi";
    }
}

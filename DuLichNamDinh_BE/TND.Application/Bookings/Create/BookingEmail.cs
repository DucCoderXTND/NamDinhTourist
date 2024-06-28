using TND.Domain.Models;

namespace TND.Application.Bookings.Create
{
    public static class BookingEmail
    {
        public static EmailRequest GetBookingEmailRequest(string toEmail, 
            IEnumerable<(string name, byte[] file)> attachments)
        {
            return new EmailRequest(
                [toEmail],
                "Đặt chỗ đã được xác nhận!",
                "Cảm ơn bạn đã đặt chỗ trên nền tảng của chúng tôi, đây là hóa đơn", attachments);
        }
    }
}

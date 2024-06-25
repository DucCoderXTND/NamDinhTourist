using TND.Domain.Interfaces.Services;

namespace TND.Infrastructure.Services.Date
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDateTimeUtc()
        {
            return DateTime.UtcNow;
        }

        public DateOnly GetCurrentDateUtc()
        {
            return DateOnly.FromDateTime(DateTime.UtcNow);
        }
    }
}

namespace TND.Domain.Exceptions
{
    public class CityWithPostOfficeExistsException(string message) : ConflictException(message)
    {
        public override string Title => "City with same post office exists";
    }
}

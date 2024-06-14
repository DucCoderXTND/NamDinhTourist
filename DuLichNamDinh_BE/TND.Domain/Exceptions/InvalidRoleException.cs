namespace TND.Domain.Exceptions
{
    public class InvalidRoleException(string message) : BadRequestException(message)
    {
        public override string Title => "Vai trò không hợp lệ";
    }
}

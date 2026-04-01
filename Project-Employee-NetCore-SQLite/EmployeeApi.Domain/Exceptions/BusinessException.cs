using System.Net;

namespace EmployeeApi.Exceptions
{
    public class BusinessException : AppException
    {
        public BusinessException(string message) : base(message, (int)HttpStatusCode.BadRequest)
        { }
    }
}

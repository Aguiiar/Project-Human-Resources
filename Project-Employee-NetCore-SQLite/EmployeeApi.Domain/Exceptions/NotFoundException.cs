using System.Net;

namespace EmployeeApi.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message) : base(message, (int)HttpStatusCode.NotFound)
        {
        }

   
    }
}

using System.Net;

namespace WebApiOnionArchitecture.Persistence.Exceptions
{
    public class UserRegistrationException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public UserRegistrationException(string message):base(message) 
        {
            Message = message;
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}

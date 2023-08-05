using System.Net;

namespace WebApiOnionArchitecture.Persistence.Exceptions
{
    public class SignInFailureException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public SignInFailureException(string message):base(message) 
        {
            Message = message;
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}

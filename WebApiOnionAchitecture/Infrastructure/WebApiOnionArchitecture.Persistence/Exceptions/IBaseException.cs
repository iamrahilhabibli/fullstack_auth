using System.Net;

namespace WebApiOnionArchitecture.Persistence.Exceptions
{
    public interface IBaseException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}

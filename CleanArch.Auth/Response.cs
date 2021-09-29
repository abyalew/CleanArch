using System.Collections.Generic;
using System.Net;

namespace CleanArch.Auth
{
    public class Response<T>
    {
        public Response(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<Response<string>> Errors { get; set; }
        public string Message { get; set; }
        public T Content { get; set; }
    }
}

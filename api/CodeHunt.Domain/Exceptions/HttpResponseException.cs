using System;
using System.Net;

namespace CodeHunt.Domain.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException()
        : this(HttpStatusCode.InternalServerError)
        {
        }

        public HttpResponseException(HttpStatusCode statusCode, string message = null)
        : base(message)
        {
            Status = statusCode.ToString();
            StatusCode = (int)statusCode;
        }

        public string Status { get; }

        public int StatusCode { get; }
    }
}

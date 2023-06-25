namespace VenatorWebApp.Services.Exceptions
{
    public class HttpResponseException : Exception
    {
        public int StatusCode { get; }

        public HttpResponseException() => StatusCode = 409;
        public HttpResponseException(string message) : base(message) => StatusCode = 409;
        public HttpResponseException(string message, int statusCode) : base(message) => StatusCode = statusCode;
    }
}

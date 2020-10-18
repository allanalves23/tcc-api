using System;
using System.Net;

namespace API.Extensions
{
    public static class ErrorExtensions
    {
        public static string GetMessage(this Exception ex) =>
            ex switch {
                Exception _ex when _ex is AggregateException => _ex.InnerException.Message,
                Exception _ex when _ex is Exception => _ex.Message,
                _ => "Ocorreu um erro interno, se persistir reporte"
            };

        public static HttpStatusCode ToStatusHttp(this Exception ex) =>
            ex switch
            {
                Exception _ex when _ex is ArgumentNullException => HttpStatusCode.NotFound,
                Exception _ex when (
                    _ex is ArgumentException
                    || _ex is InvalidOperationException
                ) => HttpStatusCode.BadRequest,
                Exception _ex when _ex is UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                Exception _ex when _ex is AggregateException => ToStatusHttp(((AggregateException)_ex).InnerException),
                _ => HttpStatusCode.InternalServerError
            };
    }
}
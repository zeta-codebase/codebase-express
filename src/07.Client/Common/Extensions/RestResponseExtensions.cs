using System.Net;
using System.Net.Mime;
using Newtonsoft.Json;
using Zeta.CodebaseExpress.Shared.Common.Extensions;
using Zeta.CodebaseExpress.Shared.Common.Responses;
using RestSharp;

namespace Zeta.CodebaseExpress.Client.Common.Extensions;

public static class RestResponseExtensions
{
    public static ResponseResult<T> ToResponseResult<T>(this RestResponse restResponse) where T : Response
    {
        var responseResult = new ResponseResult<T>();

        try
        {
            if (restResponse.IsSuccessful)
            {
                if (string.IsNullOrEmpty(restResponse.Content))
                {
                    responseResult.Result = new SuccessResponse() as T;
                }
                else
                {
                    var response = JsonConvert.DeserializeObject<T>(restResponse.Content);

                    if (response is null)
                    {
                        throw new Exception($"Failed to deserialize JSON content {restResponse.Content} into {typeof(T).Name}.");
                    }

                    responseResult.Result = response;
                }
            }
            else
            {
                responseResult.Error = CreateErrorResponse(restResponse);

                if (restResponse.ErrorException is not null)
                {
                    responseResult.Error.Exception = restResponse.ErrorException;
                }
            }
        }
        catch (Exception exception)
        {
            responseResult.Error = new CommonErrorResponse
            {
                Exception = exception,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6",
                Title = $"{exception.GetType().FullName}: {exception.Message}",
                Status = restResponse.StatusCode,
                Detail = $"{restResponse.Content} [{exception.GetType().FullName}: {exception.Message}] {exception.StackTrace}",
            };
        }

        return responseResult;
    }

    private static ErrorResponse CreateErrorResponse(RestResponse restResponse)
    {
        if (restResponse.StatusCode == 0)
        {
            return new CommonErrorResponse
            {
                Title = "Unreachable Server",
                Status = HttpStatusCode.ServiceUnavailable,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.4",
                Detail = $"Service at {restResponse.ResponseUri} is not available. Error message: {restResponse.ErrorMessage}"
            };
        }

        if (!string.IsNullOrWhiteSpace(restResponse.Content))
        {
            var errorResponse = JsonConvert.DeserializeObject<CommonErrorResponse>(restResponse.Content);

            if (errorResponse is null)
            {
                throw new Exception($"Failed to deserialize JSON content {restResponse.Content} into {nameof(CommonErrorResponse)}.");
            }

            return errorResponse;
        }

        if (restResponse.StatusCode == HttpStatusCode.NotFound)
        {
            return new CommonErrorResponse
            {
                Title = "The specified resource was not found",
                Status = HttpStatusCode.NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Detail = $"The specified resource at {restResponse.ResponseUri} was not found."
            };
        }

        if (restResponse.StatusCode == HttpStatusCode.UnsupportedMediaType)
        {
            return new CommonErrorResponse
            {
                Title = "Unsupported media type",
                Status = HttpStatusCode.UnsupportedMediaType,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.13",
                Detail = $"The server is refusing to accept the request because the media type is not supported."
            };
        }

        if (restResponse.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new CommonErrorResponse
            {
                Title = "The requested resource requires authentication.",
                Status = HttpStatusCode.Unauthorized,
                Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
                Detail = $"The server is refusing to process the request because the user is unauthorized."
            };
        }

        return new CommonErrorResponse
        {
            Title = "Internal Server Error",
            Status = HttpStatusCode.InternalServerError,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Detail = "Something went wrong."
        };

    }
}

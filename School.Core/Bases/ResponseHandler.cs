using Microsoft.Extensions.Localization;
using School.Core.SharedResources;

namespace School.Core.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<Resources> _localizer;


        public ResponseHandler(IStringLocalizer<Resources> localizer)
        {
            _localizer = localizer;
        }
        public Response<T> Deleted<T>(string? message = null)
        {

            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = message is null ? _localizer[ResourcesKeys.Deleted] : message
            };
        }
        public Response<T> Success<T>(T entity, object? Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = _localizer[ResourcesKeys.Succeeded],
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = _localizer[ResourcesKeys.UnAuthorized]
            };
        }
        public Response<T> BadRequest<T>(string? Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? _localizer[ResourcesKeys.BadRequest] : Message
            };
        }

        public Response<T> Conflict<T>(string? Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Conflict,
                Succeeded = false,
                Message = Message == null ? _localizer[ResourcesKeys.Conflict] : Message
            };
        }

        public Response<T> NotFound<T>(string? message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? _localizer[ResourcesKeys.NotFound] : message
            };
        }

        public Response<T> Created<T>(T entity, object? Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = _localizer[ResourcesKeys.Created],
                Meta = Meta,

            };
        }

        public Response<string> Created(string? message = null, object? Meta = null)
        {
            return new Response<string>()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = _localizer[ResourcesKeys.Created],
                Meta = Meta
            };
        }


        public Response<T> Updated<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = _localizer[ResourcesKeys.Updated]
            };
        }
    }

}

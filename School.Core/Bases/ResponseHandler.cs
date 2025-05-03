using Microsoft.Extensions.Localization;
using School.Core.SharedResources;

namespace School.Core.Bases
{
    public class ResponseHandler
    {
        protected readonly IStringLocalizer<Resources> localizer;


        public ResponseHandler(IStringLocalizer<Resources> localizer)
        {
            this.localizer = localizer;
        }
        public Response<T> Deleted<T>(string? message = null)
        {

            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = message is null ? localizer[ResourcesKeys.Deleted] : message
            };
        }
        public Response<T> Success<T>(T? entity, object? Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = localizer[ResourcesKeys.Succeeded],
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>(string? message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = message ?? localizer[ResourcesKeys.UnAuthorized]
            };
        }
        public Response<T> BadRequest<T>(string? Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? localizer[ResourcesKeys.BadRequest] : Message
            };
        }

        public Response<T> Conflict<T>(string? Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Conflict,
                Succeeded = false,
                Message = Message ?? localizer[ResourcesKeys.Conflict]
            };
        }

        public Response<T> NotFound<T>(string? message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message ?? localizer[ResourcesKeys.NotFound]
            };
        }

        public Response<T> Created<T>(T entity, object? Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = localizer[ResourcesKeys.Created],
                Meta = Meta,

            };
        }

        public Response<string> Created(string? message = null, object? Meta = null)
        {
            return new Response<string>()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = localizer[ResourcesKeys.Created],
                Meta = Meta
            };
        }


        public Response<T> Updated<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = localizer[ResourcesKeys.Updated]
            };
        }
    }

}

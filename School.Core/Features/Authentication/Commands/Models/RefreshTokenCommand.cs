﻿using MediatR;
using School.Core.Bases;
using School.Data.Helpers.Authentication;

namespace School.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

    }
}

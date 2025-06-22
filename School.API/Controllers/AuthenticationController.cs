﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.API.Bases;
using School.Core.Features.Authentication.Commands.Models;

namespace School.API.Controllers
{

    public class AuthenticationController : CustomControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] SignInCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost("Refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }


        [HttpGet("Confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpGet("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromQuery] ResendConfirmationEmailCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost("password-reset/send-email")]
        public async Task<IActionResult> PasswordResetEmail([FromForm] SendResetPasswordCodeCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost("password-reset/verify-code")]
        public async Task<IActionResult> VerifyPasswordResetCode([FromForm] VerifyResetPasswordCodeCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
    }
}

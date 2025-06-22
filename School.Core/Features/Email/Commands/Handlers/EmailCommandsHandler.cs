using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Email.Commands.Models;
using School.Core.SharedResources;
using School.Services.ServicesContracts;

namespace School.Core.Features.Email.Commands.Handlers
{
    public class EmailCommandsHandler : ResponseHandler, IRequestHandler<SendEmailCommand, Response<string>>
    {

        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<Resources> _stringLocalizer;



        public EmailCommandsHandler(IEmailService emailService, IStringLocalizer<Resources> stringLocalizer) : base(stringLocalizer)
        {
            _emailService = emailService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailService.SendEmail(request.ReceiverEmail, request.Message, "Confirm email");
            if (result)
                return Success();
            return BadRequest<string>("Something went wrong while sending email.");
        }
    }
}

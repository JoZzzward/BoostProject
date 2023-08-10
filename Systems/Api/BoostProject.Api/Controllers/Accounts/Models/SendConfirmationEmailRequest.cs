﻿using AutoMapper;
using BoostProject.Api.Controllers.Accounts.Models;
using BoostProject.Services.UserAccountService.Models;
using FluentValidation;

namespace BoostProject.Api.Controllers.Accounts.Models
{
    public class SendConfirmationEmailRequest
    {
        public string Email { get; set; }
    }
}

public class SendConfirmationEmailRequestValidator : AbstractValidator<SendConfirmationEmailRequest>
{
    public SendConfirmationEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");
    }
}

public class SendConfirmationEmailRequestProfile : Profile
{
    public SendConfirmationEmailRequestProfile()
    {
        CreateMap<SendConfirmationEmailRequest, SendConfirmationEmailModel>();
    }
}

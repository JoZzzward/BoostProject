﻿using AutoMapper;
using BoostProject.Services.UserAccountService.Models;
using FluentValidation;

namespace BoostProject.Api.Controllers.Accounts;

public class SendPasswordRecoveryRequest
{
    public string Email { get; set; }
}

public class PasswordRecoveryMailRequestValidator : AbstractValidator<SendPasswordRecoveryRequest>
{
    public PasswordRecoveryMailRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Incorrect email.")
            .Length(1, 50).WithMessage("Email length must be less than 50");
    }
}

public class PasswordRecoveryMailRequestProfile : Profile
{
	public PasswordRecoveryMailRequestProfile()
	{
		CreateMap<SendPasswordRecoveryRequest, SendPasswordRecoveryModel>();
	}
}
using FluentValidation;
using OrayPortfolio.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.Validations
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("2FA kodu boş olamaz")
                .Length(6).WithMessage("2FA kodu 6 haneli olmalıdır");
        }
    }
}

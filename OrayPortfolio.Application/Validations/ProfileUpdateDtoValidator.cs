using FluentValidation;
using OrayPortfolio.Application.DTOs.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.Validations
{
    public class ProfileUpdateDtoValidator : AbstractValidator<ProfileUpdateDto>
    {
        public ProfileUpdateDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad soyad boş olamaz");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email girin");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Unvan boş olamaz");

            RuleFor(x => x.ShortBio)
                .NotEmpty().WithMessage("Kısa biyografi boş olamaz")
                .MaximumLength(200).WithMessage("Kısa biyografi 200 karakteri geçemez");
            RuleFor(x => x.LongBio)
               .NotEmpty().WithMessage("Uzun biyografi boş olamaz");
        }
    }
    }


using FluentValidation;
using OrayPortfolio.Application.DTOs.Reference;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.Validations
{
    public class ReferenceUpdateDtoValidator : AbstractValidator<ReferenceCreateDto>
    {
        public ReferenceUpdateDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad soyad boş olamaz");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Pozisyon boş olamaz");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz");
        }
    }
}

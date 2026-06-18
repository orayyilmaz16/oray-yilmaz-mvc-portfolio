using FluentValidation;
using OrayPortfolio.Application.DTOs.VolunteerWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.Validations
{
    public class VolunteerWorkCreateDtoValidator : AbstractValidator<VolunteerWorkCreateDto>
    {
        public VolunteerWorkCreateDtoValidator()
        {
            RuleFor(x => x.Organization)
                .NotEmpty().WithMessage("Organizasyon adı boş olamaz");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Rol boş olamaz");
        }
    }
}

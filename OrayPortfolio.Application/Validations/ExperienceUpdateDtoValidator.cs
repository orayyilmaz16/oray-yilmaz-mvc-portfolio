using FluentValidation;
using OrayPortfolio.Application.DTOs.Experience;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.Validations
{
    public class ExperienceUpdateDtoValidator : AbstractValidator<ExperienceUpdateDto>
    {
        public ExperienceUpdateDtoValidator()
        {
            RuleFor(x => x.Company)
                .NotEmpty().WithMessage("Şirket adı boş olamaz");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Pozisyon boş olamaz");

            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("Başlangıç tarihi zorunludur");
        }
    }
}

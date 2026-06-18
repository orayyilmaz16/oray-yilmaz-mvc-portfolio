using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.Validations
{
    using FluentValidation;
    using OrayPortfolio.Application.DTOs.Education;

    public class EducationUpdateDtoValidator : AbstractValidator<EducationDto>
    {
        public EducationUpdateDtoValidator()
        {
            RuleFor(x => x.School)
                .NotEmpty().WithMessage("Okul adı boş olamaz");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Bölüm boş olamaz");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Başlangıç tarihi boş olamaz");

        

        }
    }

}

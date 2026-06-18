using FluentValidation;
using OrayPortfolio.Application.DTOs.Education;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.Validations
{
    public class EducationCreateDtoValidator : AbstractValidator<EducationCreateDto>
    {
        public EducationCreateDtoValidator()
        {
            RuleFor(x => x.School)
                .NotEmpty().WithMessage("Okul adı boş olamaz");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Bölüm boş olamaz");
        }
    }
}

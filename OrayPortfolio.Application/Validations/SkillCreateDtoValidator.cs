using FluentValidation;
using OrayPortfolio.Application.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.Validations
{
    public class SkillCreateDtoValidator : AbstractValidator<SkillCreateDto>
    {
        public SkillCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Yetenek adı boş olamaz")
                .MaximumLength(50).WithMessage("Yetenek adı en fazla 50 karakter olabilir");

            RuleFor(x => x.Level)
                .InclusiveBetween(1, 100).WithMessage("Seviye 1 ile 100 arasında olmalıdır");
        }
    }
}

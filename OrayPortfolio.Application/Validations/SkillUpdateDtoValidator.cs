using FluentValidation;
using OrayPortfolio.Application.DTOs.Skill;

namespace OrayPortfolio.Application.Validations
{
    public class SkillUpdateDtoValidator : AbstractValidator<SkillUpdateDto>
    {
        public SkillUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Yetenek adı boş olamaz")
                .MaximumLength(50).WithMessage("Yetenek adı en fazla 50 karakter olabilir");

            RuleFor(x => x.Level)
                .InclusiveBetween(1, 100).WithMessage("Seviye 1 ile 100 arasında olmalıdır");
        }
    }
}

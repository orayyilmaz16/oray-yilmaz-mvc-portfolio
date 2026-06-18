using FluentValidation;
using OrayPortfolio.Application.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.Validations
{
    public class ProjectUpdateDtoValidator : AbstractValidator<ProjectUpdateDto>
    {
        public ProjectUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Proje başlığı boş olamaz")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz");

            RuleFor(x => x.Technologies)
                .NotEmpty().WithMessage("Teknolojiler boş olamaz");
        }
    }
}

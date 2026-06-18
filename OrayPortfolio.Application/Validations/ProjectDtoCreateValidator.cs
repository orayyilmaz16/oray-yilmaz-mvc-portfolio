using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using OrayPortfolio.Application.DTOs.Project;

namespace OrayPortfolio.Application.Validations
{
   
    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Proje başlığı boş olamaz")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz");

            RuleFor(x => x.Technologies)
                .NotEmpty().WithMessage("Teknolojiler boş olamaz");
        }
    }

}

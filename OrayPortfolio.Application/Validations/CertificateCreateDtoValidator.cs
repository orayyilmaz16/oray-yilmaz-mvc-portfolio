using FluentValidation;
using OrayPortfolio.Application.DTOs.Certificate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.Validations
{
    public class CertificateCreateDtoValidator : AbstractValidator<CertificateCreateDto>
    {
        public CertificateCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Sertifika başlığı boş olamaz");

            RuleFor(x => x.Issuer)
                .NotEmpty().WithMessage("Veren kurum boş olamaz");
        }
    }
}

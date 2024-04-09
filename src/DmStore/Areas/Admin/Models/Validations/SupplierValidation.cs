using FluentValidation;

namespace DmStore.Areas.Admin.Models.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(c => c.NAME)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.CNPJ)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(14, 14).WithMessage("O campo {PropertyName} precisa ter {MinLength} caracteres");

            RuleFor(c => c.ADDRESS)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.ADDRESS_NUMBER)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(1, 10).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.ZIP_CODE)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(8, 8).WithMessage("O campo {PropertyName} precisa ter {MinLength} caracteres");

            RuleFor(c => c.NEIGHBORHOOD)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.CITY)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.STATE)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 2).WithMessage("O campo {PropertyName} precisa ter {MinLength} caracteres");
        }
    }
}

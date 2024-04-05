using DmStore.Models;
using FluentValidation;

namespace DmStore.Models.Validations
{
    public class ClientValidation : AbstractValidator<Client>
    {
        public ClientValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O campo {0} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {0} precisa ter entre {1} e {2} caracteres");

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O campo {0} precisa ser fornecido")
                .Length(11, 11).WithMessage("O campo {0} precisa ter {1} caracteres");

            RuleFor(c => c.PhoneNumber)
               .NotEmpty().WithMessage("O campo {0} precisa ser fornecido")
               .Length(11, 11).WithMessage("O campo {0} precisa ter {1} caracteres");

            RuleFor(c => c.PublicPlace)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
              .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(1, 10).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(8, 8).WithMessage("O campo {PropertyName} precisa ter {MinLength} caracteres");

            RuleFor(c => c.Neighborhood)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 2).WithMessage("O campo {PropertyName} precisa ter {MinLength} caracteres");
        }
    }
}

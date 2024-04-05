using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DmStore.Models
{
    public class Client : Entity
    {
        [Required]
        [DisplayName("Nome")]
        [MinLength(2), MaxLength(200)]
        public string? Name { get; set; }

        [DisplayName("Nome")]
        [MinLength(2), MaxLength(200)]
        public string? NormalizedName { get; set; }

        [Required]
        [MinLength(11), MaxLength(11)]
        public string? Cpf { get; set; }

        [Required]
        [DisplayName("Celular")]
        [MinLength(11), MaxLength(11)]
        public string? PhoneNumber { get; set;}

        [Required]
        [DisplayName("Logradouro")]
        [MinLength(2), MaxLength(200)]
        public string? PublicPlace { get; set; }

        [Required]
        [DisplayName("Número")]
        [MinLength(1), MaxLength(10)]
        public string? Number { get; set; }

        [DisplayName("Complemento")]
        public string? Complement { get; set; }

        [Required]
        [DisplayName("Cep")]
        [MinLength(8), MaxLength(8)]
        public string? ZipCode { get; set; }

        [Required]
        [DisplayName("Bairro")]
        [MinLength(2), MaxLength(100)]
        public string? Neighborhood { get; set; }

        [Required]
        [DisplayName("Cidade")]
        [MinLength(2), MaxLength(100)]
        public string? City { get; set; }

        [Required]
        [DisplayName("Estado")]
        [MinLength(2), MaxLength(2)]
        public string? State { get; set; }

        [DisplayName("Status")]
        public bool Active { get; set; }
    }
}

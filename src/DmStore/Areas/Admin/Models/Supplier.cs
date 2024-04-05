using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DmStore.Models;

namespace DmStore.Areas.Admin.Models
{
    public class Supplier : Entity
    {
        [Required]
        [DisplayName("Fabricante")]
        [MinLength(2), MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MinLength(14), MaxLength(14)]
        public string? Cnpj { get; set; }

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

        [Required]
        [DisplayName("Data de Cadastro")]
        public DateTime DateRegister { get; set; }

        [Required]
        [DisplayName("Data de Atualização")]
        public DateTime DateUpload { get; set; }

        [DisplayName("Status")]
        public bool Active { get; set; }

        /* EF Relations */
        public IEnumerable<Product>? Products { get; set; }
    }
}

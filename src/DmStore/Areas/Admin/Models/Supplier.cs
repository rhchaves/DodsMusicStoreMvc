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
        public string NAME { get; set; }

        [Required]
        [MinLength(14), MaxLength(14)]
        public string? CNPJ { get; set; }

        [Required]
        [DisplayName("Endereço")]
        [MinLength(2), MaxLength(200)]
        public string? ADDRESS { get; set; }

        [Required]
        [DisplayName("Número")]
        [MinLength(1), MaxLength(10)]
        public string? ADDRESS_NUMBER { get; set; }

        [DisplayName("Complemento")]
        public string? COMPLEMENT { get; set; }

        [Required]
        [DisplayName("Cep")]
        [MinLength(8), MaxLength(8)]
        public string? ZIP_CODE { get; set; }

        [Required]
        [DisplayName("Bairro")]
        [MinLength(2), MaxLength(100)]
        public string? NEIGHBORHOOD { get; set; }

        [Required]
        [DisplayName("Cidade")]
        [MinLength(2), MaxLength(100)]
        public string? CITY { get; set; }

        [Required]
        [DisplayName("Estado")]
        [MinLength(2), MaxLength(2)]
        public string? STATE { get; set; }

        [Required]
        [DisplayName("Status")]
        public bool STATUS { get; set; }

        [Required]
        [DisplayName("Atualização do Status")]
        public DateTime UPDATE_STATUS { get; set; }

        [Required]
        [DisplayName("Data de Cadastro")]
        public DateTime CREATE_REGISTER { get; set; }

        [Required]
        [DisplayName("Atualização do Cadastro")]
        public DateTime UPDATE_REGISTER { get; set; }

        /* EF Relations */
        public IEnumerable<Product>? PRODUCTS { get; set; }
    }
}

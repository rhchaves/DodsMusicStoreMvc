using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DmStore.Models;

namespace DmStore.Areas.Admin.Models
{
    public class Product : Entity
    {
        [Required]
        public string SupplierId { get; set; }

        [Required]
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [DisplayName("Descrição")]
        public string? Description { get; set; }

        [DisplayName("Imagem")]
        public string? Image { get; set; }

        [Required]
        [DisplayName("Preço")]
        public decimal? Price { get; set; }

        [Required]
        [DisplayName("Data de Cadastro")]
        public DateTime DateRegister { get; set; }

        [Required]
        [DisplayName("Data de Atualização")]
        public DateTime DateUpload { get; set; }

        [DisplayName("Status")]
        public bool Active { get; set; }

        /* EF Relations */
        public Supplier? Supplier { get; set; }
    }
}

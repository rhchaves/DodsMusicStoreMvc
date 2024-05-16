using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DmStore.Models;

namespace DmStore.Areas.Admin.Models
{
    public class Product : Entity
    {      
        [Required]
        [DisplayName("Nome")]
        public string? NAME { get; set; }

        [DisplayName("Descrição")]
        public string? DESCRIPTION { get; set; }

        public string? IMAGE_URI { get; set; }

        [NotMapped]
        [DisplayName("Imagem do Produto")]
        public IFormFile? IMAGE_UPLOAD { get; set; }

        [Required]
        [DisplayName("Preço")]
        public decimal PRICE { get; set; }
        
        [Required]
        [DisplayName("Qtd Estoque")]
        public int STOCK_QTD { get; set; }

        [Required]
        [DisplayName("Qtd Vendida")]
        public int SOLD_QTD { get; set; }

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
        [Required]
        public string SUPPLIER_ID { get; set; }

        [ForeignKey("SUPPLIER_ID")]
        public Supplier? SUPPLIER { get; set; }
    }
}

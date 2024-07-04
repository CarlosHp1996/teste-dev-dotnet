using System.ComponentModel.DataAnnotations;

namespace CarrinhoDeCompras.Entities
{
    public class ItemCarrinho
    {
        public int Id { get; set; }

        public int CarrinhoId { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        public string Produto { get; set; }

        public decimal PrecoUnitario { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoTotal { get; set; }
    }
}

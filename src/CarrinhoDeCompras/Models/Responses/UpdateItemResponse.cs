namespace CarrinhoDeCompras.Models.Responses
{
    public class UpdateItemResponse
    {
        public int Id { get; set; }
        public string? NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
    }
}

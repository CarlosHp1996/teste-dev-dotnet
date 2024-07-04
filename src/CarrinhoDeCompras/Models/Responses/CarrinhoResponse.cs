namespace CarrinhoDeCompras.Models.Responses
{
    public class CarrinhoResponse
    {
        public CreateItemResponse Item { get; set; }
        public int TotalItens { get; set; }
        public decimal ValorTotal { get; set; }
    }
}

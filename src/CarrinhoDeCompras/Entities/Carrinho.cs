namespace CarrinhoDeCompras.Entities
{
    public class Carrinho
    {
        public int Id { get; set; }
        public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
        public int TotalItens { get; set; }
        public decimal ValorTotal => Itens.Sum(item => item.PrecoTotal);
    }
}

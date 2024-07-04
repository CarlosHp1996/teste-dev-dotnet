using CarrinhoDeCompras.Entities;
using CarrinhoDeCompras.Models.Requests;
using CarrinhoDeCompras.Models.Responses;

namespace CarrinhoDeCompras.Interfaces.Repositories
{
    public interface ICarrinhoRepository
    {
        Task<List<Carrinho>> GetCarrinho();
        Task<CreateItemResponse> CreateItemCarrinho(CreateItemRequest item);
        Task RemoveItem(int id);
        Task<UpdateItemResponse> UpdateItemCarrinho(UpdateItemRequest request);
        Task<ItemCarrinho> GetCarrinhoItem(int id);
    }
}

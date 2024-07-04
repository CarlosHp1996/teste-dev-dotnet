using CarrinhoDeCompras.Entities;
using CarrinhoDeCompras.Models.Requests;
using CarrinhoDeCompras.Models.Responses;

namespace CarrinhoDeCompras.Interfaces.Services
{
    public interface ICarrinhoService
    {
        Task<List<Carrinho>> GetCarrinhoAsync();
        Task<CreateItemResponse> CreateItemCarrinhoAsync(CreateItemRequest item);
        Task RemoveItemAsync(int id);
        Task<UpdateItemResponse> UpdateItemCarrinhoAsync(UpdateItemRequest request);
        Task<ItemCarrinho> GetCarrinhoItemAsync(int id);
    }
}

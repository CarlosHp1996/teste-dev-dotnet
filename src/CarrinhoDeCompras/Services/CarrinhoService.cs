using CarrinhoDeCompras.Entities;
using CarrinhoDeCompras.Interfaces.Repositories;
using CarrinhoDeCompras.Interfaces.Services;
using CarrinhoDeCompras.Models.Requests;
using CarrinhoDeCompras.Models.Responses;

namespace CarrinhoDeCompras.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
        }

        public async Task<List<Carrinho>> GetCarrinhoAsync()
        {
            return await _carrinhoRepository.GetCarrinho();
        }

        public async Task<CreateItemResponse> CreateItemCarrinhoAsync(CreateItemRequest item)
        {
            return await _carrinhoRepository.CreateItemCarrinho(item);
        }

        public async Task RemoveItemAsync(int id)
        {
            await _carrinhoRepository.RemoveItem(id);
        }

        public async Task<UpdateItemResponse> UpdateItemCarrinhoAsync(UpdateItemRequest request)
        {
            return await _carrinhoRepository.UpdateItemCarrinho(request);
        }

        public async Task<ItemCarrinho> GetCarrinhoItemAsync(int id)
        {
            return await _carrinhoRepository.GetCarrinhoItem(id);
        }
    }
}

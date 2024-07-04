using Dapper;
using Microsoft.Data.SqlClient;
using CarrinhoDeCompras.Entities;
using CarrinhoDeCompras.Interfaces.Repositories;
using CarrinhoDeCompras.Models.Requests;
using CarrinhoDeCompras.Models.Responses;

namespace CarrinhoDeCompras.Data.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public CarrinhoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Carrinho>> GetCarrinho()
        {
            string query = @"
           SELECT * FROM Carrinho;
           SELECT * FROM Carrinho_Itens;";

            using var con = new SqlConnection(connectionString);
            var multiResult = await con.QueryMultipleAsync(query);

            var carrinhos = (await multiResult.ReadAsync<Carrinho>()).ToList();
            var itens = (await multiResult.ReadAsync<ItemCarrinho>()).ToList();

            foreach (var carrinho in carrinhos)
            {
                carrinho.Itens = itens.Where(i => i.CarrinhoId == carrinho.Id).ToList();
                carrinho.TotalItens = carrinho.Itens.Count;
            }

            return carrinhos;
        }

        public async Task<CreateItemResponse> CreateItemCarrinho(CreateItemRequest item)
        {
            if (item.PrecoUnitario < 0)
                throw new ArgumentException("O preço unitário não pode ser negativo.");
            if (item.Quantidade < 0)
                throw new ArgumentException("A quantidade não pode ser negativa.");

            string sql = @"
                    INSERT INTO Carrinho_Itens (CarrinhoId, Produto, Quantidade, PrecoUnitario, PrecoTotal)
                    VALUES (@CarrinhoId, @Produto, @Quantidade, @PrecoUnitario, @PrecoTotal);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

            using var con = new SqlConnection(connectionString);
            int novoItemId = await con.ExecuteScalarAsync<int>(sql, new
            {
                item.CarrinhoId,
                item.Produto,
                item.Quantidade,
                item.PrecoUnitario,
                item.PrecoTotal
            });

            var response = new CreateItemResponse
            {
                Id = novoItemId,
                NomeProduto = item.Produto,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.PrecoUnitario,
                PrecoTotal = item.PrecoUnitario * item.Quantidade
            };

            return response;
        }

        public async Task RemoveItem(int itemId)
        {
            string sql = "DELETE FROM Carrinho_Itens WHERE Id = @Id";
            using var con = new SqlConnection(connectionString);
            await con.ExecuteAsync(sql, new { Id = itemId });
        }

        public async Task<UpdateItemResponse> UpdateItemCarrinho(UpdateItemRequest request)
        {
            string updateSql = "UPDATE Carrinho_Itens SET Quantidade = @Quantidade, PrecoTotal = @PrecoTotal WHERE Id = @Id";
            using var con = new SqlConnection(connectionString);
            var carrinhoItem = await GetCarrinhoItem(request.Id);
            if (request.Id == 0)
                throw new ArgumentException("Item não encontrado.");
            decimal precoTotal = carrinhoItem.PrecoUnitario * request.Quantidade;

            await con.ExecuteAsync(updateSql, new
            {
                request.Quantidade,
                PrecoTotal = precoTotal,
                Id = request.Id
            });

            carrinhoItem.Quantidade = request.Quantidade;
            carrinhoItem.PrecoTotal = precoTotal;

            var response = new UpdateItemResponse
            {
                Id = carrinhoItem.Id,
                NomeProduto = carrinhoItem.Produto,
                Quantidade = carrinhoItem.Quantidade,
                PrecoUnitario = carrinhoItem.PrecoUnitario,
                PrecoTotal = carrinhoItem.PrecoTotal
            };

            return response;
        }

        public async Task<ItemCarrinho> GetCarrinhoItem(int id)
        {
            string sql = "SELECT * FROM Carrinho_Itens WHERE Id = @Id";
            using var con = new SqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<ItemCarrinho>(sql, new { Id = id });
        }
    }
}
